using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Login
{
    internal class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserCommandResult>
    {
        private readonly IUserService userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<Result<LoginUserCommandResult>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var loginResult = await userService.Login(command.Email, command.Password);
            if (loginResult.IsFailed)
            {
                return loginResult.ToResult<LoginUserCommandResult>();
            }

            var commandResult = new LoginUserCommandResult(loginResult.Value);

            return Result.Ok(commandResult);
        }
    }
}
