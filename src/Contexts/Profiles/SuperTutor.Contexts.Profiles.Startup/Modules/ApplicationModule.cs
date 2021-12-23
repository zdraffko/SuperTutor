using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Common.Commands.Decorators;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(IRequestHandler<,>));

        RegisterCommandHandlers(builder);
    }

    private void RegisterCommandHandlers(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(CreateProfileCommand).Assembly);
    }
}
