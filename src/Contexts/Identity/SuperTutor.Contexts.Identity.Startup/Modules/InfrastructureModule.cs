using Autofac;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens;
using SuperTutor.Contexts.Identity.Infrastructure.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder) => RegisterServices(builder);

    private static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<IntegrationEventsService>().As<IIntegrationEventsService>().InstancePerLifetimeScope();
    }
}
