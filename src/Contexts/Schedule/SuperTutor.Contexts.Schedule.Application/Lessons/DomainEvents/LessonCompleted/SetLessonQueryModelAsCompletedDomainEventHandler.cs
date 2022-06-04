using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonCompleted;

internal class SetLessonQueryModelAsCompletedDomainEventHandler : IDomainEventHandler<LessonCompletedDomainEvent>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public SetLessonQueryModelAsCompletedDomainEventHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task Handle(LessonCompletedDomainEvent domainEvent, CancellationToken cancellationToken)
        => await lessonQueryModelRepository.SetAsCompleted(domainEvent.LessonId, domainEvent.Status, cancellationToken);
}
