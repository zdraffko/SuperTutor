namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterStudent;

public class RegisterStudentCommandResult
{
    public RegisterStudentCommandResult(string authToken) => AuthToken = authToken;

    public string AuthToken { get; }
}
