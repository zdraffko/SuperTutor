using Autofac;
using MassTransit;
using SuperTutor.Contexts.Profiles.Application.IntegrationEventConsumers.Identity;
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

                rabbitmqConfigurator.ReceiveEndpoint("user-deleted-queue", endpointConfigurator =>
                {
                    endpointConfigurator.ConfigureConsumer<UserDeletedIntegrationEventConsumer>(busRegistrationContext);
                });
            });
        });
    }
}
