using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Delete;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Login;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Identity.Api.Controllers;

public class UsersController : ApiController
{
    private readonly ICommandHandler<LoginUserCommand, LoginUserCommandResult> loginUserCommandHandler;
    private readonly ICommandHandler<RegisterUserCommand> registerUserCommandHandler;
    private readonly ICommandHandler<DeleteUserCommand> deleteUserCommandHandler;

    public UsersController(
        ICommandHandler<LoginUserCommand, LoginUserCommandResult> loginUserCommandHandler,
        ICommandHandler<RegisterUserCommand> registerUserCommandHandler,
        ICommandHandler<DeleteUserCommand> deleteUserCommandHandler)
    {
        this.loginUserCommandHandler = loginUserCommandHandler;
        this.registerUserCommandHandler = registerUserCommandHandler;
        this.deleteUserCommandHandler = deleteUserCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult<LoginUserCommandResult>> Login(LoginUserCommand command, CancellationToken cancellationToken)
        => await Handle(loginUserCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        => await Handle(registerUserCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Delete(DeleteUserCommand command, CancellationToken cancellationToken)
        => await Handle(deleteUserCommandHandler, command, cancellationToken);
}
