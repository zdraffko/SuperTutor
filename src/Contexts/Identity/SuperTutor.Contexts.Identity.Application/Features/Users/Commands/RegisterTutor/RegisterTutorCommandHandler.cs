using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterTutor;

internal class RegisterTutorCommandHandler : ICommandHandler<RegisterTutorCommand, RegisterTutorCommandResult>
{
    private readonly IUserService userService;

    public RegisterTutorCommandHandler(IUserService userService) => this.userService = userService;

    public async Task<Result<RegisterTutorCommandResult>> Handle(RegisterTutorCommand command, CancellationToken cancellationToken)
    {
        var registerResult = await userService.RegisterTutor(command.Email, command.Password);
        if (registerResult.IsFailed)
        {
            return registerResult.ToResult<RegisterTutorCommandResult>();
        }

        var loginResult = await userService.Login(command.Email, command.Password);
        if (loginResult.IsFailed)
        {
            return loginResult.ToResult<RegisterTutorCommandResult>();
        }

        var commandResult = new RegisterTutorCommandResult(loginResult.Value);

        return Result.Ok(commandResult);
    }
}
