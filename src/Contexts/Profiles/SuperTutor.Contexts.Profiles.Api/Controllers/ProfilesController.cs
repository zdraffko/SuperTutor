using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Activate;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.AddTutoringGrades;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Approve;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Deactivate;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.DecreaseRateForOneHour;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Delete;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.IncreaseRateForOneHour;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RemoveTutoringGrades;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RequestRedaction;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.SubmitForReview;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.UpdateAbout;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Api.Controllers;

public class ProfilesController : ApiController
{
    private readonly ICommandHandler<CreateTutorProfileCommand> createProfileCommandHandler;
    private readonly ICommandHandler<DeleteTutorProfileCommand> deleteProfileCommandHandler;
    private readonly ICommandHandler<ApproveTutorProfileCommand> approveProfileCommandHandler;
    private readonly ICommandHandler<RequestTutorProfileRedactionCommand> requestProfileRedactionCommandHandler;
    private readonly ICommandHandler<SubmitTutorProfileForReviewCommand> submitProfileForReviewCommandHandler;
    private readonly ICommandHandler<ActivateTutorProfileCommand> activateProfileCommandHandler;
    private readonly ICommandHandler<DeactivateTutorProfileCommand> deactivateProfileCommandHandler;
    private readonly ICommandHandler<UpdateTutorProfileAboutCommand> updateProfileAboutCommandHandler;
    private readonly ICommandHandler<AddTutoringGradesToTutorProfileCommand> addTutoringGradesToProfileCommandHandler;
    private readonly ICommandHandler<RemoveTutoringGradesFromTutorProfileCommand> removeTutoringGradesFromProfileCommandHandler;
    private readonly ICommandHandler<IncreaseTutorProfileRateForOneHourCommand> increaseProfileRateForOneHourCommandHandelr;
    private readonly ICommandHandler<DecreaseTutorProfileRateForOneHourCommand> decreaseProfileRateForOneHourCommandHandler;

    public ProfilesController(
        ICommandHandler<CreateTutorProfileCommand> createProfileCommandHandler,
        ICommandHandler<DeleteTutorProfileCommand> deleteProfileCommandHandler,
        ICommandHandler<ApproveTutorProfileCommand> approveProfileCommandHandler,
        ICommandHandler<RequestTutorProfileRedactionCommand> requestProfileRedactionCommandHandler,
        ICommandHandler<SubmitTutorProfileForReviewCommand> submitProfileForReviewCommandHandler,
        ICommandHandler<ActivateTutorProfileCommand> activateProfileCommandHandler,
        ICommandHandler<DeactivateTutorProfileCommand> deactivateProfileCommandHandler,
        ICommandHandler<UpdateTutorProfileAboutCommand> updateProfileAboutCommandHandler,
        ICommandHandler<AddTutoringGradesToTutorProfileCommand> addTutoringGradesToProfileCommandHandler,
        ICommandHandler<RemoveTutoringGradesFromTutorProfileCommand> removeTutoringGradesFromProfileCommandHandler,
        ICommandHandler<IncreaseTutorProfileRateForOneHourCommand> increaseProfileRateForOneHourCommandHandelr,
        ICommandHandler<DecreaseTutorProfileRateForOneHourCommand> decreaseProfileRateForOneHourCommandHandler)
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
    public async Task<ActionResult> Create(CreateTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(createProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Delete(DeleteTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(deleteProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Approve(ApproveTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(approveProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RequestRedaction(RequestTutorProfileRedactionCommand command, CancellationToken cancellationToken)
        => await Handle(requestProfileRedactionCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> SubmitForReview(SubmitTutorProfileForReviewCommand command, CancellationToken cancellationToken)
        => await Handle(submitProfileForReviewCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Activate(ActivateTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(activateProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Deactivate(DeactivateTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(deactivateProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateAbout(UpdateTutorProfileAboutCommand command, CancellationToken cancellationToken)
        => await Handle(updateProfileAboutCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> AddTutoringGrades(AddTutoringGradesToTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(addTutoringGradesToProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveTutoringGrades(RemoveTutoringGradesFromTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(removeTutoringGradesFromProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> IncreaseRateForOneHour(IncreaseTutorProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(increaseProfileRateForOneHourCommandHandelr, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> DecreaseRateForOneHour(DecreaseTutorProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(decreaseProfileRateForOneHourCommandHandler, command, cancellationToken);
}
