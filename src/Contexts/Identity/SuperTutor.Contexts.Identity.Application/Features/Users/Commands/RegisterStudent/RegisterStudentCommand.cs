using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterStudent;

public class RegisterStudentCommand : Command<RegisterStudentCommandResult>
{
    public RegisterStudentCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }

    public string Password { get; }
}
