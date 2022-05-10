using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Delete;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Login;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterStudent;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterTutor;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Api.Controllers;

public class UsersController : ApiController
{
    private readonly ICommandHandler<LoginUserCommand, LoginUserCommandResult> loginUserCommandHandler;
    private readonly ICommandHandler<RegisterTutorCommand, RegisterTutorCommandResult> registerTutorCommandHandler;
    private readonly ICommandHandler<RegisterStudentCommand, RegisterStudentCommandResult> registerStudentCommandHandler;
    private readonly ICommandHandler<DeleteUserCommand> deleteUserCommandHandler;

    public UsersController(
        ICommandHandler<LoginUserCommand, LoginUserCommandResult> loginUserCommandHandler,
        ICommandHandler<RegisterTutorCommand, RegisterTutorCommandResult> registerTutorCommandHandler,
        ICommandHandler<RegisterStudentCommand, RegisterStudentCommandResult> registerStudentCommandHandler,
        ICommandHandler<DeleteUserCommand> deleteUserCommandHandler)
    {
        this.loginUserCommandHandler = loginUserCommandHandler;
        this.registerTutorCommandHandler = registerTutorCommandHandler;
        this.registerStudentCommandHandler = registerStudentCommandHandler;
        this.deleteUserCommandHandler = deleteUserCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult<LoginUserCommandResult>> Login(LoginUserCommand command, CancellationToken cancellationToken)
        => await Handle(loginUserCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult<RegisterTutorCommandResult>> RegisterTutor(RegisterTutorCommand command, CancellationToken cancellationToken)
        => await Handle(registerTutorCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult<RegisterStudentCommandResult>> RegisterStudent(RegisterStudentCommand command, CancellationToken cancellationToken)
        => await Handle(registerStudentCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Delete(DeleteUserCommand command, CancellationToken cancellationToken)
        => await Handle(deleteUserCommandHandler, command, cancellationToken);
}
