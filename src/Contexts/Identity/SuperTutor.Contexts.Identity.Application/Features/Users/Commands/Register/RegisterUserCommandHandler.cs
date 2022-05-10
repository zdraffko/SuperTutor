using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
{
    private readonly IUserService userService;

    public RegisterUserCommandHandler(IUserService userService) => this.userService = userService;

    public async Task<Result<RegisterUserCommandResult>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var registerResult = await userService.Register(command.Email, command.Username, command.Password);
        if (registerResult.IsFailed)
        {
            return registerResult.ToResult<RegisterUserCommandResult>();
        }

        var loginResult = await userService.Login(command.Email, command.Password);
        if (loginResult.IsFailed)
        {
            return loginResult.ToResult<RegisterUserCommandResult>();
        }

        var commandResult = new RegisterUserCommandResult(loginResult.Value);

        return Result.Ok(commandResult);
    }
}
