using Microsoft.AspNetCore.Identity;

namespace SuperTutor.Contexts.Identity.Persistence.Entities;

public class User : IdentityUser<int>
{
    public User(string email, string userName) : base(userName)
    {
        Email = email;
    }
}
