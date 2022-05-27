using Microsoft.AspNetCore.Identity;
using SuperTutor.Contexts.Identity.Domain.Users.Models.Enumerations;

namespace SuperTutor.Contexts.Identity.Domain.Users;

public class User : IdentityUser<Guid>
{
    public User(string email, UserType type, string firstName, string lastName) : base(email)
    {
        Email = email;
        Type = type;
        FirstName = firstName;
        LastName = lastName;
    }

    public UserType Type { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
