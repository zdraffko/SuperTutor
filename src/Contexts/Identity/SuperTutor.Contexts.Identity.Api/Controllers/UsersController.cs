using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Delete;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Login;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterAdmin;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterStudent;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterTutor;
using SuperTutor.Contexts.Identity.Application.Features.Users.Queries.GetIdentityInfo;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Identity.Api.Controllers;

public class UsersController : ApiController
{
    private readonly ICommandHandler<LoginUserCommand, LoginUserCommandResult> loginUserCommandHandler;
    private readonly ICommandHandler<RegisterTutorCommand, RegisterTutorCommandResult> registerTutorCommandHandler;
    private readonly ICommandHandler<RegisterStudentCommand, RegisterStudentCommandResult> registerStudentCommandHandler;
    private readonly ICommandHandler<RegisterAdminCommand, RegisterAdminCommandResult> registerAdminCommandHandler;
    private readonly ICommandHandler<DeleteUserCommand> deleteUserCommandHandler;
    private readonly IQueryHandler<GetUserIdentityInfoQuery, GetUserIdentityInfoQueryPayload> getUserIdentityInfoQueryHandler;

    public UsersController(
        ICommandHandler<LoginUserCommand, LoginUserCommandResult> loginUserCommandHandler,
        ICommandHandler<RegisterTutorCommand, RegisterTutorCommandResult> registerTutorCommandHandler,
        ICommandHandler<RegisterStudentCommand, RegisterStudentCommandResult> registerStudentCommandHandler,
        ICommandHandler<RegisterAdminCommand, RegisterAdminCommandResult> registerAdminCommandHandler,
        ICommandHandler<DeleteUserCommand> deleteUserCommandHandler,
        IQueryHandler<GetUserIdentityInfoQuery, GetUserIdentityInfoQueryPayload> getUserIdentityInfoQueryHandler)
    {
        this.loginUserCommandHandler = loginUserCommandHandler;
        this.registerTutorCommandHandler = registerTutorCommandHandler;
        this.registerStudentCommandHandler = registerStudentCommandHandler;
        this.registerAdminCommandHandler = registerAdminCommandHandler;
        this.deleteUserCommandHandler = deleteUserCommandHandler;
        this.getUserIdentityInfoQueryHandler = getUserIdentityInfoQueryHandler;
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
    public async Task<ActionResult<RegisterAdminCommandResult>> RegisterAdmin(RegisterAdminCommand command, CancellationToken cancellationToken)
        => await Handle(registerAdminCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Delete(DeleteUserCommand command, CancellationToken cancellationToken)
        => await Handle(deleteUserCommandHandler, command, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetUserIdentityInfoQueryPayload>> GetIdentityInfo([FromJsonQuery] GetUserIdentityInfoQuery query, CancellationToken cancellationToken)
        => await Handle(getUserIdentityInfoQueryHandler, query, cancellationToken);
}
