using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Identity.Api.Controllers;

public class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator) { }
    
    [HttpGet]
    public ActionResult Test()
    {
        return new OkObjectResult("test result");
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);
}
