﻿using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Schedule.Application.Lessons.Commands.Complete;
using SuperTutor.Contexts.Schedule.Application.Lessons.Commands.ReserveTrialLesson;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForStudent;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Api.Lessons.Controllers;

public class LessonsController : ApiController
{
    private readonly ICommandHandler<ReserveTrialLessonCommand, ReserveTrialLessonCommandPayload> reserveTrialLessonCommandHandler;
    private readonly ICommandHandler<CompleteLessonCommand> completeLessonCommandHandler;
    private readonly IQueryHandler<GetScheduledLessonsForStudentQuery, GetScheduledLessonsForStudentQueryPayload> getScheduledLessonsForStudentQueryHandler;

    public LessonsController(
        ICommandHandler<ReserveTrialLessonCommand, ReserveTrialLessonCommandPayload> reserveTrialLessonCommandHandler,
        ICommandHandler<CompleteLessonCommand> completeLessonCommandHandler,
        IQueryHandler<GetScheduledLessonsForStudentQuery, GetScheduledLessonsForStudentQueryPayload> getScheduledLessonsForStudentQueryHandler)
    {
        this.reserveTrialLessonCommandHandler = reserveTrialLessonCommandHandler;
        this.completeLessonCommandHandler = completeLessonCommandHandler;
        this.getScheduledLessonsForStudentQueryHandler = getScheduledLessonsForStudentQueryHandler;
    }

    [HttpPost]
    public async Task<ActionResult<ReserveTrialLessonCommandPayload>> ReserveTrialLesson(ReserveTrialLessonCommand command, CancellationToken cancellationToken)
        => await Handle(reserveTrialLessonCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Complete(CompleteLessonCommand command, CancellationToken cancellationToken)
        => await Handle(completeLessonCommandHandler, command, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetScheduledLessonsForStudentQueryPayload>> GetScheduledLessonsForStudent([FromJsonQuery] GetScheduledLessonsForStudentQuery query, CancellationToken cancellationToken)
        => await Handle(getScheduledLessonsForStudentQueryHandler, query, cancellationToken);
}
