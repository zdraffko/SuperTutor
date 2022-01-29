using Autofac;
using SuperTutor.Contexts.Identity.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents.Commands.Decorators;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterCommandHandlers(builder);
    }

    private void RegisterCommandHandlers(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IIdentityApplicationAssemblyMarker).Assembly).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IIdentityApplicationAssemblyMarker).Assembly).AsClosedTypesOf(typeof(ICommandHandler<,>)).InstancePerLifetimeScope();

        builder.RegisterGenericDecorator(typeof(IntegrationEventsCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(IntegrationEventsCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));
    }
}
