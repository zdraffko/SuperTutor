﻿using Autofac;
using SuperTutor.Contexts.Identity.Api;
using SuperTutor.Contexts.Identity.Application;
using SuperTutor.Contexts.Identity.Infrastructure;
using SuperTutor.Contexts.Identity.Persistence;
using SuperTutor.SharedLibraries.BuildingBlocks.Api;
using SuperTutor.SharedLibraries.BuildingBlocks.Application;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class ConventionsBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new[]
        {
            typeof(IIdentityApiAssemblyMarker).Assembly,
            typeof(IIdentityApplicationAssemblyMarker).Assembly,
            typeof(IIdentityInfrastructureAssemblyMarker).Assembly,
            typeof(IIdentityPersistenceAssemblyMarker).Assembly,

            typeof(IBuildingBlocksApiAssemblyMarker).Assembly,
            typeof(IBuildingBlocksApplicationAssemblyMarker).Assembly,
            typeof(IBuildingBlocksInfrastructureAssemblyMarker).Assembly,
            typeof(IBuildingBlocksPersistenceAssemblyMarker).Assembly
        };

        builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandHandler<,>)).InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandValidator<>)).InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandValidator<,>)).InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().PreserveExistingDefaults().InstancePerLifetimeScope();
    }
}
