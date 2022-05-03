using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

public interface IDomainEventDispatcher
{
    Task Dispatch<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken) where TDomainEvent : DomainEvent;
}
