namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterTutor;

public class RegisterTutorCommandResult
{
    public RegisterTutorCommandResult(string authToken) => AuthToken = authToken;

    public string AuthToken { get; }
}
