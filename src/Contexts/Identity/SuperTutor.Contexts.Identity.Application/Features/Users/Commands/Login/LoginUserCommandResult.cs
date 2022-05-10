namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Login;

public class LoginUserCommandResult
{
    public LoginUserCommandResult(string authToken) => AuthToken = authToken;

    public string AuthToken { get; }
}
