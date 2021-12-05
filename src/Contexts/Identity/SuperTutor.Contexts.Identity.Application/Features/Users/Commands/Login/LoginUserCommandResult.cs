namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Login
{
    public class LoginUserCommandResult
    {
        public LoginUserCommandResult(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
