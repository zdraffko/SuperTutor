using Microsoft.AspNetCore.Identity;
using SuperTutor.Contexts.Identity.Domain.Users.Models.Enumerations;

namespace SuperTutor.Contexts.Identity.Domain.Users;

public class User : IdentityUser<Guid>
{
    public User(string email, UserType type) : base(email)
    {
        Email = email;
        Type = type;
    }

    public UserType Type { get; set; }
}
