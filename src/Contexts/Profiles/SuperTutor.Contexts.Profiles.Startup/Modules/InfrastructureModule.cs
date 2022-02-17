using Autofac;
using MassTransit;
using SuperTutor.Contexts.Profiles.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class InfrastructureModule : Module
{
    private readonly IConfiguration configuration;

    public InfrastructureModule(IConfiguration configuration) => this.configuration = configuration;

    protected override void Load(ContainerBuilder builder)
    {
        RegisterServices(builder);
        RegisterMasstransit(builder);
    }

    private void RegisterServices(ContainerBuilder builder)
        => builder.RegisterType<IntegrationEventsService>().As<IIntegrationEventsService>().InstancePerLifetimeScope();

    private void RegisterMasstransit(ContainerBuilder builder)
        => builder.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumers(typeof(IProfilesInfrastructureAssemblyMarker).Assembly);

            busConfigurator.UsingRabbitMq((busRegistrationContext, rabbitmqConfigurator) =>
            {
                rabbitmqConfigurator.Host(configuration["RabbitMq:Url"]);

                var consumers = typeof(IProfilesInfrastructureAssemblyMarker).Assembly
                    .GetTypes()
                    .Where(type => type.IsAssignableTo(typeof(IConsumer)));

                foreach (var consumer in consumers)
                {
                    rabbitmqConfigurator.ReceiveEndpoint(consumer.FullName, endpointConfigurator => endpointConfigurator.ConfigureConsumer(busRegistrationContext, consumer));
                }
            });
        });
}
