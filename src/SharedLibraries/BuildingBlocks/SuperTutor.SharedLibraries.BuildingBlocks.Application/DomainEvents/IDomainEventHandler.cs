using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

public interface IDomainEventHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
