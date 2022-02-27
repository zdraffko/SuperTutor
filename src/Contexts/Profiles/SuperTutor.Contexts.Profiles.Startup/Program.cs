using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using SuperTutor.Contexts.Profiles.Api;
using SuperTutor.Contexts.Profiles.Persistence.Contexts;
using SuperTutor.Contexts.Profiles.Startup.Modules;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;

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
                IndexFormat = $"logs-supertutor-profiles-{DateTime.UtcNow:yyyy-MM-dd}",
                TypeName = null,
                BatchAction = ElasticOpType.Create,
                ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(hostBuilderContext.Configuration["Elasticsearch:Username"], hostBuilderContext.Configuration["Elasticsearch:Password"]),
                BufferBaseFilename = "./logs/elasticsearch/buffer"
            })
            .ReadFrom.Configuration(hostBuilderContext.Configuration));

    // Add services to the container.

    builder.Services
        .AddControllers()
        .AddJsonOptions(jsonOptions => jsonOptions.JsonSerializerOptions.Converters.Add(new IdentifierJsonConverterFactory()))
        .AddApplicationPart(typeof(IProfilesApiAssemblyMarker).Assembly)
        .AddControllersAsServices();

    builder.Services.AddDbContext<ProfilesDbContext>(options => options.UseSqlServer(builder.Configuration["Database:ConnectionString"]));

    builder.Services.AddMassTransitHostedService();

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder
        => containerBuilder
            .RegisterModule(new ApplicationModule())
            .RegisterModule(new InfrastructureModule(builder.Configuration))
            .RegisterModule(new PersistenceModule()));

    var app = builder.Build();

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
