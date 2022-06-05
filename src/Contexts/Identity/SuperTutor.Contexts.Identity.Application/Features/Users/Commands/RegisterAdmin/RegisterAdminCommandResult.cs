namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterAdmin;

public class RegisterAdminCommandResult
{
    public RegisterAdminCommandResult(string authToken) => AuthToken = authToken;

    public string AuthToken { get; }
}
