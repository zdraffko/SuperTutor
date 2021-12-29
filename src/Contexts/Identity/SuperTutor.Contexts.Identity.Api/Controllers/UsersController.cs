using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Delete;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Login;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Identity.Api.Controllers;

public class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator) { }
    
    [HttpPost]
    public async Task<ActionResult<LoginUserCommandResult>> Login(LoginUserCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Delete(DeleteUserCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);
}
