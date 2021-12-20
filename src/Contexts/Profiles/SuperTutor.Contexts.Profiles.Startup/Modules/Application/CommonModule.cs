using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;

namespace SuperTutor.Contexts.Profiles.Startup.Modules.Application;

internal class CommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(CreateProfileCommand).Assembly);
    }
}
