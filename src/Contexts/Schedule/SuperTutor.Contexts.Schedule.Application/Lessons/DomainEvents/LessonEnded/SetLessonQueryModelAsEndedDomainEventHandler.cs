using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonEnded;

internal class SetLessonQueryModelAsEndedDomainEventHandler : IDomainEventHandler<LessonEndedDomainEvent>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public SetLessonQueryModelAsEndedDomainEventHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task Handle(LessonEndedDomainEvent domainEvent, CancellationToken cancellationToken)
        => await lessonQueryModelRepository.SetAsEnded(domainEvent.LessonId, domainEvent.Status, cancellationToken);
}
