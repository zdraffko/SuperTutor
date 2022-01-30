﻿using Autofac;
using SuperTutor.Contexts.Profiles.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents.Commands.Decorators;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.UnitOfWork.Commands.Decorators;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterCommandHandlers(builder);
    }

    private void RegisterCommandHandlers(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IProfilesApplicationAssemblyMarker).Assembly).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IProfilesApplicationAssemblyMarker).Assembly).AsClosedTypesOf(typeof(ICommandHandler<,>)).InstancePerLifetimeScope();

        builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));

        builder.RegisterGenericDecorator(typeof(IntegrationEventsCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(IntegrationEventsCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));
    }
}
