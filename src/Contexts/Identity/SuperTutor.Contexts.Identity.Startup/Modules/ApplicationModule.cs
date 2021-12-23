using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterCommandHandlers(builder);
    }

    private void RegisterCommandHandlers(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(RegisterUserCommand).Assembly);
    }
}
