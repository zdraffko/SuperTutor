using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonAbandoned;

internal class SetLessonQueryModelAsAbandonedDomainEventHandler : IDomainEventHandler<LessonAbandonedDomainEvent>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public SetLessonQueryModelAsAbandonedDomainEventHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task Handle(LessonAbandonedDomainEvent domainEvent, CancellationToken cancellationToken)
        => await lessonQueryModelRepository.SetAsAbandoned(domainEvent.LessonId, domainEvent.Status, domainEvent.PaymentStatus, cancellationToken);
}
