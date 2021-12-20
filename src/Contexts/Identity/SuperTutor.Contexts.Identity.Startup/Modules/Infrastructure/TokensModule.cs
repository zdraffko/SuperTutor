using Autofac;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens;

namespace SuperTutor.Contexts.Identity.Startup.Modules.Infrastructure;

internal class TokensModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TokenService>().As<ITokenService>();
    }
}
