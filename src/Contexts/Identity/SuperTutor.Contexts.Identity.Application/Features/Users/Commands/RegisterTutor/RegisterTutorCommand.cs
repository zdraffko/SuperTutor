using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterTutor;

public class RegisterTutorCommand : Command<RegisterTutorCommandResult>
{
    public RegisterTutorCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }

    public string Password { get; }
}
