using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Activate;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.AddTutoringGrades;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Approve;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Deactivate;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.DecreaseRateForOneHour;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Delete;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.IncreaseRateForOneHour;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RemoveTutoringGrades;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RequestRedaction;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.SubmitForReview;
using SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.UpdateAbout;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Api.Controllers;

public class ProfilesController : ApiController
{
    private readonly ICommandHandler<CreateProfileCommand> createProfileCommandHandler;
    private readonly ICommandHandler<DeleteProfileCommand> deleteProfileCommandHandler;
    private readonly ICommandHandler<ApproveProfileCommand> approveProfileCommandHandler;
    private readonly ICommandHandler<RequestProfileRedactionCommand> requestProfileRedactionCommandHandler;
    private readonly ICommandHandler<SubmitProfileForReviewCommand> submitProfileForReviewCommandHandler;
    private readonly ICommandHandler<ActivateProfileCommand> activateProfileCommandHandler;
    private readonly ICommandHandler<DeactivateProfileCommand> deactivateProfileCommandHandler;
    private readonly ICommandHandler<UpdateProfileAboutCommand> updateProfileAboutCommandHandler;
    private readonly ICommandHandler<AddTutoringGradesToProfileCommand> addTutoringGradesToProfileCommandHandler;
    private readonly ICommandHandler<RemoveTutoringGradesFromProfileCommand> removeTutoringGradesFromProfileCommandHandler;
    private readonly ICommandHandler<IncreaseProfileRateForOneHourCommand> increaseProfileRateForOneHourCommandHandelr;
    private readonly ICommandHandler<DecreaseProfileRateForOneHourCommand> decreaseProfileRateForOneHourCommandHandler;

    public ProfilesController(
        ICommandHandler<CreateProfileCommand> createProfileCommandHandler,
        ICommandHandler<DeleteProfileCommand> deleteProfileCommandHandler,
        ICommandHandler<ApproveProfileCommand> approveProfileCommandHandler,
        ICommandHandler<RequestProfileRedactionCommand> requestProfileRedactionCommandHandler,
        ICommandHandler<SubmitProfileForReviewCommand> submitProfileForReviewCommandHandler,
        ICommandHandler<ActivateProfileCommand> activateProfileCommandHandler,
        ICommandHandler<DeactivateProfileCommand> deactivateProfileCommandHandler,
        ICommandHandler<UpdateProfileAboutCommand> updateProfileAboutCommandHandler,
        ICommandHandler<AddTutoringGradesToProfileCommand> addTutoringGradesToProfileCommandHandler,
        ICommandHandler<RemoveTutoringGradesFromProfileCommand> removeTutoringGradesFromProfileCommandHandler,
        ICommandHandler<IncreaseProfileRateForOneHourCommand> increaseProfileRateForOneHourCommandHandelr,
        ICommandHandler<DecreaseProfileRateForOneHourCommand> decreaseProfileRateForOneHourCommandHandler)
    {
        this.createProfileCommandHandler = createProfileCommandHandler;
        this.deleteProfileCommandHandler = deleteProfileCommandHandler;
        this.approveProfileCommandHandler = approveProfileCommandHandler;
        this.requestProfileRedactionCommandHandler = requestProfileRedactionCommandHandler;
        this.submitProfileForReviewCommandHandler = submitProfileForReviewCommandHandler;
        this.activateProfileCommandHandler = activateProfileCommandHandler;
        this.deactivateProfileCommandHandler = deactivateProfileCommandHandler;
        this.updateProfileAboutCommandHandler = updateProfileAboutCommandHandler;
        this.addTutoringGradesToProfileCommandHandler = addTutoringGradesToProfileCommandHandler;
        this.removeTutoringGradesFromProfileCommandHandler = removeTutoringGradesFromProfileCommandHandler;
        this.increaseProfileRateForOneHourCommandHandelr = increaseProfileRateForOneHourCommandHandelr;
        this.decreaseProfileRateForOneHourCommandHandler = decreaseProfileRateForOneHourCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProfileCommand command, CancellationToken cancellationToken)
        => await Handle(createProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Delete(DeleteProfileCommand command, CancellationToken cancellationToken)
        => await Handle(deleteProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Approve(ApproveProfileCommand command, CancellationToken cancellationToken)
        => await Handle(approveProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RequestRedaction(RequestProfileRedactionCommand command, CancellationToken cancellationToken)
        => await Handle(requestProfileRedactionCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> SubmitForReview(SubmitProfileForReviewCommand command, CancellationToken cancellationToken)
        => await Handle(submitProfileForReviewCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Activate(ActivateProfileCommand command, CancellationToken cancellationToken)
        => await Handle(activateProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Deactivate(DeactivateProfileCommand command, CancellationToken cancellationToken)
        => await Handle(deactivateProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateAbout(UpdateProfileAboutCommand command, CancellationToken cancellationToken)
        => await Handle(updateProfileAboutCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> AddTutoringGrades(AddTutoringGradesToProfileCommand command, CancellationToken cancellationToken)
        => await Handle(addTutoringGradesToProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveTutoringGrades(RemoveTutoringGradesFromProfileCommand command, CancellationToken cancellationToken)
        => await Handle(removeTutoringGradesFromProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> IncreaseRateForOneHour(IncreaseProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(increaseProfileRateForOneHourCommandHandelr, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> DecreaseRateForOneHour(DecreaseProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(decreaseProfileRateForOneHourCommandHandler, command, cancellationToken);
}
