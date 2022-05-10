using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterStudent;

internal class RegisterStudentCommandHandler : ICommandHandler<RegisterStudentCommand, RegisterStudentCommandResult>
{
    private readonly IUserService userService;

    public RegisterStudentCommandHandler(IUserService userService) => this.userService = userService;

    public async Task<Result<RegisterStudentCommandResult>> Handle(RegisterStudentCommand command, CancellationToken cancellationToken)
    {
        var registerResult = await userService.RegisterStudent(command.Email, command.Password);
        if (registerResult.IsFailed)
        {
            return registerResult.ToResult<RegisterStudentCommandResult>();
        }

        var loginResult = await userService.Login(command.Email, command.Password);
        if (loginResult.IsFailed)
        {
            return loginResult.ToResult<RegisterStudentCommandResult>();
        }

        var commandResult = new RegisterStudentCommandResult(loginResult.Value);

        return Result.Ok(commandResult);
    }
}
