using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonScheduled;

internal class SetLessonQueryModelAsScheduledDomainEventHandler : IDomainEventHandler<LessonScheduledDomainEvent>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public SetLessonQueryModelAsScheduledDomainEventHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task Handle(LessonScheduledDomainEvent domainEvent, CancellationToken cancellationToken)
        => await lessonQueryModelRepository.SetAsScheduled(domainEvent.LessonId, domainEvent.Status, domainEvent.PaymentStatus, cancellationToken);
}
