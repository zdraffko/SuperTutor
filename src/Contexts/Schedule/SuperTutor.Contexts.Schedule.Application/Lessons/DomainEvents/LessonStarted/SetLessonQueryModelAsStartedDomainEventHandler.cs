using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonStarted;

internal class SetLessonQueryModelAsStartedDomainEventHandler : IDomainEventHandler<LessonStartedDomainEvent>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public SetLessonQueryModelAsStartedDomainEventHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task Handle(LessonStartedDomainEvent domainEvent, CancellationToken cancellationToken)
        => await lessonQueryModelRepository.SetAsStarted(domainEvent.LessonId, domainEvent.Status, cancellationToken);
}
