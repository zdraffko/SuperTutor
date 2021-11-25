using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;
using SuperTutor.Contexts.Identity.Infrastructure.Users;
using SuperTutor.Contexts.Identity.Persistence.Entities;

namespace SuperTutor.Contexts.Identity.Startup.IocContainerModules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(RegisterUserCommand).Assembly);

        builder.RegisterType<UserService>().As<IUserService>();
        // builder.RegisterType<UserManager<User>>();
    }
}
