using SuperTutor.Contexts.Identity.Domain.Users;

namespace SuperTutor.Contexts.Identity.Infrastructure.Tokens;

public interface ITokenService
{
    Task<string> GenerateToken(User user);
}
