using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Activate;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.AddTutoringGrades;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Approve;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Deactivate;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.DecreaseRateForOneHour;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.IncreaseRateForOneHour;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RemoveTutoringGrades;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RequestRedaction;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.SubmitForReview;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.UpdateAbout;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Profiles.Api.Controllers;

public class ProfilesController : ApiController
{
    public ProfilesController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProfileCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Approve(ApproveProfileCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RequestRedaction(RequestProfileRedactionCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> SubmitForReview(SubmitProfileForReviewCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Activate(ActivateProfileCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Deactivate(DeactivateProfileCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateAbout(UpdateProfileAboutCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> AddTutoringGrades(AddTutoringGradesToProfileCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveTutoringGrades(RemoveTutoringGradesFromProfileCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> IncreaseRateForOneHour(IncreaseProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> DecreaseRateForOneHour(DecreaseProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(command, cancellationToken);
}
