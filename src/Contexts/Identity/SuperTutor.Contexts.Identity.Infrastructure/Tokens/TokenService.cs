using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SuperTutor.Contexts.Identity.Domain.Users;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperTutor.Contexts.Identity.Infrastructure.Tokens;

public class TokenService : ITokenService
{
    private readonly string SecretKey;

    public TokenService(IOptionsSnapshot<AuthTokenOptions> authTokenOptions) => SecretKey = authTokenOptions.Value.SecretKey;

    public async Task<string> GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = Encoding.ASCII.GetBytes(SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Type.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encryptedToken = tokenHandler.WriteToken(token);

        return await Task.FromResult(encryptedToken);
    }
}
