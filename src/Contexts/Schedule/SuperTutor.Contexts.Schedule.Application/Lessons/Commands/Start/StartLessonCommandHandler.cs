using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.Start;

internal class StartLessonCommandHandler : ICommandHandler<StartLessonCommand>
{
    private readonly IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public StartLessonCommandHandler(IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository, IIntegrationEventsService integrationEventsService)
    {
        this.lessonRepository = lessonRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(StartLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.Load(command.LessonId, cancellationToken);
        if (lesson is null)
        {
            return Result.Fail($"Lesson with Id {command.LessonId} was not found");
        }

        lesson.Start();

        await lessonRepository.Update(lesson, cancellationToken);

        integrationEventsService.Raise(new LessonStartedIntegrationEvent(
            lessonId: lesson.Id.Value,
            tutorId: lesson.TutorId.Value,
            studentId: lesson.StudentId.Value));

        return Result.Ok();
    }
}
