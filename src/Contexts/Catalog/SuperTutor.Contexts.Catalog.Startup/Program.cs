using Autofac;
using Autofac.Extensions.DependencyInjection;
using Elastic.CommonSchema.Serilog;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using SuperTutor.Contexts.Catalog.Api;
using SuperTutor.Contexts.Catalog.Infrastructure;
using SuperTutor.Contexts.Catalog.Persistence.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.HealthChecks.Extensions;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var selfLogFileWriter = TextWriter.Synchronized(File.CreateText("/app/logs/serilog-selflog"));

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
                IndexFormat = $"logs-supertutor-catalog",
                TypeName = null, // This is needed because the _type field for a document has been deprecated and there is no clean way at the moment to configure the Elasticsearch sink to not send it. See https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374 and https://www.elastic.co/guide/en/elasticsearch/reference/7.17/removal-of-types.html
                BatchAction = ElasticOpType.Create, // This is needed in order to use data streams instead of aliases https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374
                ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(hostBuilderContext.Configuration["Elasticsearch:Username"], hostBuilderContext.Configuration["Elasticsearch:Password"]),
                BufferBaseFilename = "./logs/elasticsearch/buffer",
                CustomFormatter = new EcsTextFormatter()
            })
            .ReadFrom.Configuration(hostBuilderContext.Configuration));

    builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration["Database:ConnectionString"], name: "Database", healthQuery: "select top (1) [Id] from catalog.Students")
        .AddRabbitMQ(builder.Configuration["RabbitMq:Url"], name: "RabbitMq")
        .AddElasticsearch(options => options
                .UseServer(elasticsearchNodeUrls.First().AbsoluteUri)
                .UseBasicAuthentication(builder.Configuration["Elasticsearch:Username"], builder.Configuration["Elasticsearch:Password"]),
        "Elasticsearch");

    // Add library services to the container via extension methods provided by the libraries.

    builder.Services
        .AddControllers()
        .AddJsonOptions(jsonOptions => jsonOptions.JsonSerializerOptions.Converters.Add(new IdentifierJsonConverterFactory()))
        .AddApplicationPart(typeof(ICatalogApiAssemblyMarker).Assembly)
        .AddControllersAsServices();

    builder.Services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(builder.Configuration["Database:ConnectionString"]));

    builder.Services.AddMassTransit(busConfigurator =>
     {
         busConfigurator.AddConsumers(typeof(ICatalogInfrastructureAssemblyMarker).Assembly);

         busConfigurator.UsingRabbitMq((busRegistrationContext, rabbitmqConfigurator) =>
         {
             rabbitmqConfigurator.Host(builder.Configuration["RabbitMq:Url"]);

             var consumers = typeof(ICatalogInfrastructureAssemblyMarker).Assembly
                 .GetTypes()
                 .Where(type => type.IsAssignableTo(typeof(IConsumer)));

             foreach (var consumer in consumers)
             {
                 rabbitmqConfigurator.ReceiveEndpoint(consumer.FullName!, endpointConfigurator => endpointConfigurator.ConfigureConsumer(busRegistrationContext, consumer));
             }
         });
     });

    // Add owned service to the container via Autofac modules.

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly));

    var app = builder.Build();

    if (builder.Environment.EnvironmentName == "AcceptanceTesting")
    {
        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        dataContext.Database.Migrate();
    }

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
