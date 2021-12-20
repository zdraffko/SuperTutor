using Autofac;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Infrastructure.Users;

namespace SuperTutor.Contexts.Identity.Startup.Modules.Infrastructure;

internal class UsersModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserService>().As<IUserService>();
    }
}
