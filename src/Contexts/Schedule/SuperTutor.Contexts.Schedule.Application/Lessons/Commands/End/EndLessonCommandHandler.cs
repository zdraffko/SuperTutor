using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.End;

internal class EndLessonCommandHandler : ICommandHandler<EndLessonCommand>
{
    private readonly IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository;

    public EndLessonCommandHandler(IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository) => this.lessonRepository = lessonRepository;

    public async Task<Result> Handle(EndLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.Load(command.LessonId, cancellationToken);
        if (lesson is null)
        {
            return Result.Fail($"Lesson with Id {command.LessonId} was not found");
        }

        lesson.End();

        await lessonRepository.Update(lesson, cancellationToken);

        return Result.Ok();
    }
}
