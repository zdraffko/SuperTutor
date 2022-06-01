using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.ReserveTrialLesson;

internal class ReserveTrialLessonCommandHandler : ICommandHandler<ReserveTrialLessonCommand, ReserveTrialLessonCommandPayload>
{
    private readonly IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository;

    public ReserveTrialLessonCommandHandler(IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository) => this.lessonRepository = lessonRepository;

    public async Task<Result<ReserveTrialLessonCommandPayload>> Handle(ReserveTrialLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = Lesson.ReserveTrialLesson(
            command.TutorId,
            command.StudentId,
            command.Date,
            command.StartTime,
            command.Subject,
            command.Grade);

        await lessonRepository.Add(lesson, cancellationToken);

        var commandPayload = new ReserveTrialLessonCommandPayload(lesson.Id);

        return Result.Ok(commandPayload);
    }
}
