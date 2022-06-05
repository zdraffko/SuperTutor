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
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForReview;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForTutor;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Profiles.Api.Controllers;

public class TutorProfilesController : ApiController
{
    private readonly ICommandHandler<CreateTutorProfileCommand, CreateTutorProfileCommandPayload> createTutorProfileCommandHandler;
    private readonly ICommandHandler<DeleteTutorProfileCommand> deleteTutorProfileCommandHandler;
    private readonly ICommandHandler<ApproveTutorProfileCommand> approveTutorProfileCommandHandler;
    private readonly ICommandHandler<RequestTutorProfileRedactionCommand> requestTutorProfileRedactionCommandHandler;
    private readonly ICommandHandler<SubmitTutorProfileForReviewCommand> submitTutorProfileForReviewCommandHandler;
    private readonly ICommandHandler<ActivateTutorProfileCommand> activateTutorProfileCommandHandler;
    private readonly ICommandHandler<DeactivateTutorProfileCommand> deactivateTutorProfileCommandHandler;
    private readonly ICommandHandler<UpdateTutorProfileAboutCommand> updateTutorProfileAboutCommandHandler;
    private readonly ICommandHandler<AddTutoringGradesToTutorProfileCommand> addTutoringGradesToTutorProfileCommandHandler;
    private readonly ICommandHandler<RemoveTutoringGradesFromTutorProfileCommand> removeTutoringGradesFromTutorProfileCommandHandler;
    private readonly ICommandHandler<IncreaseTutorProfileRateForOneHourCommand> increaseTutorProfileRateForOneHourCommandHandler;
    private readonly ICommandHandler<DecreaseTutorProfileRateForOneHourCommand> decreaseTutorProfileRateForOneHourCommandHandler;
    private readonly IQueryHandler<GetAllTutorProfilesForTutorQuery, GetAllTutorProfilesForTutorQueryPayload> getAllTutorProfilesForTutorQueryHandler;
    private readonly IQueryHandler<GetAllTutorProfilesForReviewQuery, GetAllTutorProfilesForReviewQueryPayload> getAllTutorProfilesForReviewQueryHandler;

    public TutorProfilesController(
        ICommandHandler<CreateTutorProfileCommand, CreateTutorProfileCommandPayload> createTutorProfileCommandHandler,
        ICommandHandler<DeleteTutorProfileCommand> deleteTutorProfileCommandHandler,
        ICommandHandler<ApproveTutorProfileCommand> approveTutorProfileCommandHandler,
        ICommandHandler<RequestTutorProfileRedactionCommand> requestTutorProfileRedactionCommandHandler,
        ICommandHandler<SubmitTutorProfileForReviewCommand> submitTutorProfileForReviewCommandHandler,
        ICommandHandler<ActivateTutorProfileCommand> activateTutorProfileCommandHandler,
        ICommandHandler<DeactivateTutorProfileCommand> deactivateTutorProfileCommandHandler,
        ICommandHandler<UpdateTutorProfileAboutCommand> updateTutorProfileAboutCommandHandler,
        ICommandHandler<AddTutoringGradesToTutorProfileCommand> addTutoringGradesToTutorProfileCommandHandler,
        ICommandHandler<RemoveTutoringGradesFromTutorProfileCommand> removeTutoringGradesFromTutorProfileCommandHandler,
        ICommandHandler<IncreaseTutorProfileRateForOneHourCommand> increaseTutorProfileRateForOneHourCommandHandler,
        ICommandHandler<DecreaseTutorProfileRateForOneHourCommand> decreaseTutorProfileRateForOneHourCommandHandler,
        IQueryHandler<GetAllTutorProfilesForTutorQuery, GetAllTutorProfilesForTutorQueryPayload> getAllTutorProfilesForTutorQueryHandler,
        IQueryHandler<GetAllTutorProfilesForReviewQuery, GetAllTutorProfilesForReviewQueryPayload> getAllTutorProfilesForReviewQueryHandler)
    {
        this.createTutorProfileCommandHandler = createTutorProfileCommandHandler;
        this.deleteTutorProfileCommandHandler = deleteTutorProfileCommandHandler;
        this.approveTutorProfileCommandHandler = approveTutorProfileCommandHandler;
        this.requestTutorProfileRedactionCommandHandler = requestTutorProfileRedactionCommandHandler;
        this.submitTutorProfileForReviewCommandHandler = submitTutorProfileForReviewCommandHandler;
        this.activateTutorProfileCommandHandler = activateTutorProfileCommandHandler;
        this.deactivateTutorProfileCommandHandler = deactivateTutorProfileCommandHandler;
        this.updateTutorProfileAboutCommandHandler = updateTutorProfileAboutCommandHandler;
        this.addTutoringGradesToTutorProfileCommandHandler = addTutoringGradesToTutorProfileCommandHandler;
        this.removeTutoringGradesFromTutorProfileCommandHandler = removeTutoringGradesFromTutorProfileCommandHandler;
        this.increaseTutorProfileRateForOneHourCommandHandler = increaseTutorProfileRateForOneHourCommandHandler;
        this.decreaseTutorProfileRateForOneHourCommandHandler = decreaseTutorProfileRateForOneHourCommandHandler;
        this.getAllTutorProfilesForTutorQueryHandler = getAllTutorProfilesForTutorQueryHandler;
        this.getAllTutorProfilesForReviewQueryHandler = getAllTutorProfilesForReviewQueryHandler;
    }

    [HttpPost]
    public async Task<ActionResult<CreateTutorProfileCommandPayload>> Create(CreateTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(createTutorProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Delete(DeleteTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(deleteTutorProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Approve(ApproveTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(approveTutorProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RequestRedaction(RequestTutorProfileRedactionCommand command, CancellationToken cancellationToken)
        => await Handle(requestTutorProfileRedactionCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> SubmitForReview(SubmitTutorProfileForReviewCommand command, CancellationToken cancellationToken)
        => await Handle(submitTutorProfileForReviewCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Activate(ActivateTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(activateTutorProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Deactivate(DeactivateTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(deactivateTutorProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateAbout(UpdateTutorProfileAboutCommand command, CancellationToken cancellationToken)
        => await Handle(updateTutorProfileAboutCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> AddTutoringGrades(AddTutoringGradesToTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(addTutoringGradesToTutorProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveTutoringGrades(RemoveTutoringGradesFromTutorProfileCommand command, CancellationToken cancellationToken)
        => await Handle(removeTutoringGradesFromTutorProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> IncreaseRateForOneHour(IncreaseTutorProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(increaseTutorProfileRateForOneHourCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> DecreaseRateForOneHour(DecreaseTutorProfileRateForOneHourCommand command, CancellationToken cancellationToken)
        => await Handle(decreaseTutorProfileRateForOneHourCommandHandler, command, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetAllTutorProfilesForTutorQueryPayload>> GetAllForTutor([FromJsonQuery] GetAllTutorProfilesForTutorQuery query, CancellationToken cancellationToken)
        => await Handle(getAllTutorProfilesForTutorQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetAllTutorProfilesForReviewQueryPayload>> GetAllForReview([FromJsonQuery] GetAllTutorProfilesForReviewQuery query, CancellationToken cancellationToken)
        => await Handle(getAllTutorProfilesForReviewQueryHandler, query, cancellationToken);
}
