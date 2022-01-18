using Autofac;
using MassTransit;
using SuperTutor.Contexts.Profiles.Infrastructure.IntegrationEvents.Consumers.Identity;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Contracts;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterServices(builder);
        RegisterMasstransit(builder);
    }

    private void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<IntegrationEventsService>().As<IIntegrationEventsService>().InstancePerLifetimeScope();
    }

    private void RegisterMasstransit(ContainerBuilder builder)
    {
        builder.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumers(typeof(UserDeletedIntegrationEventConsumer).Assembly);

            busConfigurator.UsingRabbitMq((busRegistrationContext, rabbitmqConfigurator) =>
            {
                rabbitmqConfigurator.Host("amqp://devuser:devPass123!@supertutor-rabbitmq:5672");

                var consumers = typeof(UserDeletedIntegrationEventConsumer).Assembly
                    .GetTypes()
                    .Where(type => type.IsAssignableTo(typeof(IConsumer)));

                foreach (var consumer in consumers)
                {
                    rabbitmqConfigurator.ReceiveEndpoint(consumer.FullName, endpointConfigurator =>
                    {
                        endpointConfigurator.ConfigureConsumer(busRegistrationContext, consumer);
                    });
                }
            });
        });
    }
}
