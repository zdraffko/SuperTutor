using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterAdmin;

internal class RegisterAdminCommandHandler : ICommandHandler<RegisterAdminCommand, RegisterAdminCommandResult>
{
    private readonly IUserService userService;

    public RegisterAdminCommandHandler(IUserService userService) => this.userService = userService;

    public async Task<Result<RegisterAdminCommandResult>> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
    {
        var registerResult = await userService.RegisterAdmin(command.Email, command.Password, command.FirstName, command.LastName);
        if (registerResult.IsFailed)
        {
            return registerResult.ToResult<RegisterAdminCommandResult>();
        }

        var loginResult = await userService.Login(command.Email, command.Password);
        if (loginResult.IsFailed)
        {
            return loginResult.ToResult<RegisterAdminCommandResult>();
        }

        var commandResult = new RegisterAdminCommandResult(loginResult.Value);

        return Result.Ok(commandResult);
    }
}
