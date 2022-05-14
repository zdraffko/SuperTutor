using Autofac;
using Autofac.Extensions.DependencyInjection;
using Elastic.CommonSchema.Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using SuperTutor.Contexts.Classroom.Api;
using SuperTutor.Contexts.Classroom.Api.VideoConferences.Hubs;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.HealthChecks.Extensions;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using System.Text;

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
                IndexFormat = $"logs-supertutor-classroom",
                TypeName = null, // This is needed because the _type field for a document has been deprecated and there is no clean way at the moment to configure the Elasticsearch sink to not send it. See https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374 and https://www.elastic.co/guide/en/elasticsearch/reference/7.17/removal-of-types.html
                BatchAction = ElasticOpType.Create, // This is needed in order to use data streams instead of aliases. See https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375#issuecomment-743372374 and https://www.elastic.co/guide/en/elasticsearch/reference/current/data-streams.html
                ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(hostBuilderContext.Configuration["Elasticsearch:Username"], hostBuilderContext.Configuration["Elasticsearch:Password"]),
                BufferBaseFilename = "./logs/elasticsearch/buffer",
                CustomFormatter = new EcsTextFormatter()
            })
            .ReadFrom.Configuration(hostBuilderContext.Configuration));

    builder.Services.AddHealthChecks()
        .AddElasticsearch(options => options
                .UseServer(elasticsearchNodeUrls.First().AbsoluteUri)
                .UseBasicAuthentication(builder.Configuration["Elasticsearch:Username"], builder.Configuration["Elasticsearch:Password"]),
        "Elasticsearch");

    // Add library services to the container via extension methods provided by the libraries.

    builder.Services
        .AddControllers()
        .AddJsonOptions(jsonOptions => jsonOptions.JsonSerializerOptions.Converters.Add(new IdentifierJsonConverterFactory()))
        .AddApplicationPart(typeof(IClassroomApiAssemblyMarker).Assembly)
        .AddControllersAsServices();

    var key = Encoding.ASCII.GetBytes(builder.Configuration["AuthTokenSecretKey"]);

    builder.Services
        .AddAuthentication(authentication =>
        {
            authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(bearer =>
        {
            bearer.RequireHttpsMetadata = false;
            bearer.SaveToken = true;
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    builder.Services.AddSignalR();

    // Add owned service to the container via Autofac modules.

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly));

    builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", corsPolicyBuilder
        => corsPolicyBuilder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials())
    );

    var app = builder.Build();

    app.UseHttpsRedirection();

    app.UseHealthChecks("/health", new HealthCheckOptions().AddCustomResponseWriter());

    // Configure the HTTP request pipeline.

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseCors("CorsPolicy");

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => endpoints.MapControllers());

    app.MapHub<SignalingHub>("/hubs/signaling");

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
