using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using SuperTutor.Contexts.Identity.Api;
using SuperTutor.Contexts.Identity.Persistence;
using SuperTutor.Contexts.Identity.Persistence.Entities;
using SuperTutor.Contexts.Identity.Startup.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration)
    => loggerConfiguration
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(hostBuilderContext.Configuration["Elasticsearch:Url"]))
        {
            IndexFormat = $"supertutor-logs-identity-{DateTime.UtcNow:yyyy-MM}",
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
    .AddApplicationPart(typeof(IIdentityApiAssemblyMarker).Assembly)
    .AddControllersAsServices();

builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(builder.Configuration["Database:ConnectionString"]));

builder.Services
    .AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddMassTransitHostedService();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder
    => containerBuilder
        .RegisterModule(new ApplicationModule())
        .RegisterModule(new InfrastructureModule(builder.Configuration)));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
