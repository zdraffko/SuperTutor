using Microsoft.AspNetCore.Identity;

namespace SuperTutor.Contexts.Identity.Infrastructure.Persistence.Entities;

public class User : IdentityUser<Guid>
{
    public User(string email, string userName) : base(userName) => Email = email;
}
