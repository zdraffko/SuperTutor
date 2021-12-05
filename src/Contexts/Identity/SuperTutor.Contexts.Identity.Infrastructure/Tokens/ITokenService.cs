using SuperTutor.Contexts.Identity.Persistence.Entities;

namespace SuperTutor.Contexts.Identity.Infrastructure.Tokens;

public interface ITokenService
{
    Task<string> GenerateToken(User user);
}
