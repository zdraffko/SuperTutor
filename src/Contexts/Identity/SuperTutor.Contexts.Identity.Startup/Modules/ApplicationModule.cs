using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens;
using SuperTutor.Contexts.Identity.Infrastructure.Users;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(RegisterUserCommand).Assembly);

        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<TokenService>().As<ITokenService>();
    }
}
