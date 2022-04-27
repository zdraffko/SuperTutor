using Autofac;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Errors.Commands.Decorators;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents.Commands.Decorators;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands.Decorators;

namespace SuperTutor.Contexts.Schedule.Startup.Modules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder) => RegisterCommandHandlerDecorators(builder);

    private static void RegisterCommandHandlerDecorators(ContainerBuilder builder)
    {
        builder.RegisterGenericDecorator(typeof(IntegrationEventsCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(IntegrationEventsCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));

        builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));

        builder.RegisterGenericDecorator(typeof(ErrorCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        builder.RegisterGenericDecorator(typeof(ErrorCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));
    }
}
