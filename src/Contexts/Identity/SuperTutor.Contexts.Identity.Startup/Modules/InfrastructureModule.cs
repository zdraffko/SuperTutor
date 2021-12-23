using Autofac;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens;
using SuperTutor.Contexts.Identity.Infrastructure.Users;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterServices(builder);
    }

    private void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<TokenService>().As<ITokenService>();
        builder.RegisterType<UserService>().As<IUserService>();
    }
}
