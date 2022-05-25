using Autofac;
using SuperTutor.Contexts.Payments.Domain;
using SuperTutor.Contexts.Schedule.Startup.BackgroundServices;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Subscriptions;

namespace SuperTutor.Contexts.Schedule.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // The services here are registered explicitly because of the singleton lifestyle scope

        builder.RegisterType<EventStoreSubscriptionBackgroundService>()
            .As<IHostedService>()
            .SingleInstance();

        builder.RegisterType<EventStoreSubscriber>()
            .As<IEventStoreSubscriber>()
            .SingleInstance();

        builder.RegisterType<EventStoreSubscriptionCheckpointRepository>()
            .As<IEventStoreSubscriptionCheckpointRepository>()
            .SingleInstance();

        builder.Register(_ => new DomainEventSerializer(typeof(IPaymentsDomainAssemblyMarker).Assembly))
            .As<IDomainEventSerializer>()
            .SingleInstance();
    }
}
