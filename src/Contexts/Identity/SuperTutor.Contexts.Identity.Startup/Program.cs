using Autofac;
using Autofac.Extensions.DependencyInjection;
using Elastic.CommonSchema.Serilog;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using SuperTutor.Contexts.Identity.Api;
using SuperTutor.Contexts.Identity.Domain.Users;
using SuperTutor.Contexts.Identity.Infrastructure.Persistence;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.HealthChecks.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var selfLogFileWriter = TextWriter.Synchronized(File.CreateText("./logs/serilog-selflog"));

    SelfLog.Enable(message =>
    {
        Console.WriteLine(message);

        selfLogFileWriter.WriteLine(message);
        selfLogFileWriter.Flush();
    });

    var elasticsearchNodeUrls = builder.Configuration["Elasticsearch:Urls"]
        .Split(';', StringSplitOptions.RemoveEmptyEntries)
        .Select(rawElasticsearchNodeUrl => new Uri(rawElasticsearchNodeUrl))
        .ToList();

    builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration)
        => loggerConfiguration
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchNodeUrls)
            {
                IndexFormat = $"logs-supertutor-identity",
                TypeName = null, // This is needed because the _type field for a document has been deprecated and there is no clean way at the moment to configure the Elasticsearch sink to not send it. See https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374 and https://www.elastic.co/guide/en/elasticsearch/reference/7.17/removal-of-types.html
                BatchAction = ElasticOpType.Create, // This is needed in order to use data streams instead of aliases. See https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374 and https://www.elastic.co/guide/en/elasticsearch/reference/current/data-streams.html
                ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(hostBuilderContext.Configuration["Elasticsearch:Username"], hostBuilderContext.Configuration["Elasticsearch:Password"]),
                BufferBaseFilename = "./logs/elasticsearch/buffer",
                CustomFormatter = new EcsTextFormatter()
            })
            .ReadFrom.Configuration(hostBuilderContext.Configuration));

    builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration["Database:ConnectionString"], name: "Database", healthQuery: "select top (1) [Id] from [identity].Users")
        .AddRabbitMQ(builder.Configuration["RabbitMq:Url"], name: "RabbitMq")
        .AddElasticsearch(options => options
                .UseServer(elasticsearchNodeUrls.First().AbsoluteUri)
                .UseBasicAuthentication(builder.Configuration["Elasticsearch:Username"], builder.Configuration["Elasticsearch:Password"]),
        "Elasticsearch");

    // Add library services to the container via extension methods provided by the libraries.

    builder.Services
        .AddControllers()
        .AddApplicationPart(typeof(IIdentityApiAssemblyMarker).Assembly)
        .AddControllersAsServices();

    builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(builder.Configuration["Database:ConnectionString"]));

    builder.Services.AddMassTransit(busConfigurator
            => busConfigurator.UsingRabbitMq((context, rabbitmqConfigurator)
                => rabbitmqConfigurator.Host(builder.Configuration["RabbitMq:Url"])));

    builder.Services
        .AddIdentity<User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<IdentityDbContext>();

    builder.Services.Configure<AuthTokenOptions>(builder.Configuration.GetSection(AuthTokenOptions.SectionName));

    // Add owned service to the container via Autofac modules.

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly));

    var app = builder.Build();

    app.UseHealthChecks("/health", new HealthCheckOptions().AddCustomResponseWriter());

    // Configure the HTTP request pipeline.

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseEndpoints(endpoints => endpoints.MapControllers());

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "An unhandled exception was thrown with message {ErrorMessage}", exception.Message);
}
finally
{
    Log.CloseAndFlush();
}
