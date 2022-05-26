using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Payments.Domain;
using SuperTutor.Contexts.Payments.Infrastructure.Shared.Persistence;
using SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence.Models.TutorQuery;
using SuperTutor.Contexts.Payments.Startup.BackgroundServices;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Serializers;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Persistence.Subscriptions;

namespace SuperTutor.Contexts.Payments.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PaymentsDbContext>()
            .As<DbContext>()
            .As<ITutorQueryModelDbContext>()
            .InstancePerLifetimeScope();

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
