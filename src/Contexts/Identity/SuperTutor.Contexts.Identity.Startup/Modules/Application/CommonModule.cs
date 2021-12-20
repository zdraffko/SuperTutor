using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;

namespace SuperTutor.Contexts.Identity.Startup.Modules.Application;

internal class CommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(RegisterUserCommand).Assembly);
    }
}
