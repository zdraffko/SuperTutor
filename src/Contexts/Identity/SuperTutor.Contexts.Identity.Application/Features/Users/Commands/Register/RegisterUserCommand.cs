using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;

public class RegisterUserCommand : Command
{
    public RegisterUserCommand(string email, string username, string password)
    {
        Email = email;
        Username = username;
        Password = password;
    }

    public string Email { get; }

    public string Username { get; }

    public string Password { get; }
}
