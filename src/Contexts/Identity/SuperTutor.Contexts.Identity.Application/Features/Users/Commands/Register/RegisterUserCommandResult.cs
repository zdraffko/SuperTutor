namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;

public class RegisterUserCommandResult
{
    public RegisterUserCommandResult(string authToken) => AuthToken = authToken;

    public string AuthToken { get; }
}
