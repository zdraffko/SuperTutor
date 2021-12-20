using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.SubmitForReview;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Profiles.Api.Controllers;

public class ProfilesController : ApiController
{
    public ProfilesController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProfileCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> SubmitForReview(SubmitProfileForReviewCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);
}
