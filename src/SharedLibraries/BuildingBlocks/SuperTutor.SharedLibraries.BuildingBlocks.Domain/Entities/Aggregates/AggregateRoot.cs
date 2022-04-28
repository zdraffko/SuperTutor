using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;

public abstract class AggregateRoot<TIdentifier, TIdentifierValue> : Entity<TIdentifier, TIdentifierValue>, IAggregateRoot
    where TIdentifier : Identifier<TIdentifierValue>
    where TIdentifierValue : struct
{
    private readonly List<DomainEvent> domainEvents;

    protected AggregateRoot(TIdentifier id) : base(id) => domainEvents = new List<DomainEvent>();

    public IReadOnlyCollection<DomainEvent> DomainEvents => domainEvents;

    protected void RaiseDomainEvent(DomainEvent domainEvent) => domainEvents.Add(domainEvent);

    public abstract void ApplyDomainEvent(DomainEvent domainEvent);
}
