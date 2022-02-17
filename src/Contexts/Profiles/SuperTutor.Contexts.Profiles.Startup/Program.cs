using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using SuperTutor.Contexts.Profiles.Api;
using SuperTutor.Contexts.Profiles.Persistence.Contexts;
using SuperTutor.Contexts.Profiles.Startup.Modules;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration)
    => loggerConfiguration
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(hostBuilderContext.Configuration["Elasticsearch:Url"]))
        {
            IndexFormat = $"supertutor-logs-profiles-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            DetectElasticsearchVersion = true,
            TypeName = null,
            BatchAction = ElasticOpType.Create,
            ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(hostBuilderContext.Configuration["Elasticsearch:Username"], hostBuilderContext.Configuration["Elasticsearch:Password"]),
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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
