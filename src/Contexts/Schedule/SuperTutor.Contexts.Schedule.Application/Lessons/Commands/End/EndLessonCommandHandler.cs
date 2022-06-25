using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.End;

internal class EndLessonCommandHandler : ICommandHandler<EndLessonCommand>
{
    private readonly IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public EndLessonCommandHandler(IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository, IIntegrationEventsService integrationEventsService)
    {
        this.lessonRepository = lessonRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(EndLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.Load(command.LessonId, cancellationToken);
        if (lesson is null)
        {
            return Result.Fail($"Lesson with Id {command.LessonId} was not found");
        }

        lesson.End();

        await lessonRepository.Update(lesson, cancellationToken);

        integrationEventsService.Raise(new LessonEndedIntegrationEvent(lessonId: lesson.Id.Value));

        return Result.Ok();
    }
}
