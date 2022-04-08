using Autofac;
using Autofac.Extensions.DependencyInjection;
using Elastic.CommonSchema.Serilog;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using SuperTutor.Contexts.Profiles.Api;
using SuperTutor.Contexts.Profiles.Infrastructure;
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
                IndexFormat = $"logs-supertutor-profiles",
                TypeName = null, // This is needed because the _type field for a document has been deprecated and there is no clean way at the moment to configure the Elasticsearch sink to not send it. See https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374 and https://www.elastic.co/guide/en/elasticsearch/reference/7.17/removal-of-types.html
                BatchAction = ElasticOpType.Create, // This is needed in order to use data streams instead of aliases https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374
                ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(hostBuilderContext.Configuration["Elasticsearch:Username"], hostBuilderContext.Configuration["Elasticsearch:Password"]),
                BufferBaseFilename = "./logs/elasticsearch/buffer",
                CustomFormatter = new EcsTextFormatter()
            })
            .ReadFrom.Configuration(hostBuilderContext.Configuration));

    // Add services to the container.

    builder.Services
        .AddControllers()
        .AddJsonOptions(jsonOptions => jsonOptions.JsonSerializerOptions.Converters.Add(new IdentifierJsonConverterFactory()))
        .AddApplicationPart(typeof(IProfilesApiAssemblyMarker).Assembly)
        .AddControllersAsServices();

    builder.Services.AddDbContext<ProfilesDbContext>(options => options.UseSqlServer(builder.Configuration["Database:ConnectionString"]));

    builder.Services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.AddConsumers(typeof(IProfilesInfrastructureAssemblyMarker).Assembly);

        busConfigurator.UsingRabbitMq((busRegistrationContext, rabbitmqConfigurator) =>
        {
            rabbitmqConfigurator.Host(builder.Configuration["RabbitMq:Url"]);

            var consumers = typeof(IProfilesInfrastructureAssemblyMarker).Assembly
                .GetTypes()
                .Where(type => type.IsAssignableTo(typeof(IConsumer)));

            foreach (var consumer in consumers)
            {
                rabbitmqConfigurator.ReceiveEndpoint(consumer.FullName!, endpointConfigurator => endpointConfigurator.ConfigureConsumer(busRegistrationContext, consumer));
            }
        });
    });

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder
        => containerBuilder
            .RegisterModule(new ApplicationModule())
            .RegisterModule(new InfrastructureModule())
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
