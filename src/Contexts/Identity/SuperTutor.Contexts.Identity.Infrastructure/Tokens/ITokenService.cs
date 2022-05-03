using SuperTutor.Contexts.Identity.Infrastructure.Persistence.Entities;

namespace SuperTutor.Contexts.Identity.Infrastructure.Tokens;

public interface ITokenService
{
    Task<string> GenerateToken(User user);
}
