using Autofac;
using MassTransit;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens;
using SuperTutor.Contexts.Identity.Infrastructure.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents;

namespace SuperTutor.Contexts.Identity.Startup.Modules;

internal class InfrastructureModule : Module
{
    private readonly IConfiguration configuration;

    public InfrastructureModule(IConfiguration configuration) => this.configuration = configuration;

    protected override void Load(ContainerBuilder builder)
    {
        RegisterServices(builder);
        RegisterMasstransit(builder);
    }

    private void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<IntegrationEventsService>().As<IIntegrationEventsService>().InstancePerLifetimeScope();
    }

    private void RegisterMasstransit(ContainerBuilder builder)
        => builder.AddMassTransit(busConfigurator
            => busConfigurator.UsingRabbitMq((context, rabbitmqConfigurator)
                => rabbitmqConfigurator.Host(configuration["RabbitMq:Url"])));
}
