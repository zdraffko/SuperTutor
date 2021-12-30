using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Common.Commands.Decorators;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents.Commands.Decorators;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterCommandHandlers(builder);
    }

    private void RegisterCommandHandlers(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(CreateProfileCommand).Assembly);
        builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(IRequestHandler<,>));
        builder.RegisterGenericDecorator(typeof(IntegrationEventsCommandHandlerDecorator<>), typeof(IRequestHandler<,>));
    }
}
