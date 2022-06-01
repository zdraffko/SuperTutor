using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Schedule.Application.Lessons.Commands.ReserveTrialLesson;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Api.Lessons.Controllers;

public class LessonsController : ApiController
{
    private readonly ICommandHandler<ReserveTrialLessonCommand, ReserveTrialLessonCommandPayload> reserveTrialLessonCommandHandler;

    public LessonsController(ICommandHandler<ReserveTrialLessonCommand, ReserveTrialLessonCommandPayload> reserveTrialLessonCommandHandler) => this.reserveTrialLessonCommandHandler = reserveTrialLessonCommandHandler;

    [HttpPost]
    public async Task<ActionResult<ReserveTrialLessonCommandPayload>> ReserveTrialLesson(ReserveTrialLessonCommand command, CancellationToken cancellationToken)
        => await Handle(reserveTrialLessonCommandHandler, command, cancellationToken);
}
