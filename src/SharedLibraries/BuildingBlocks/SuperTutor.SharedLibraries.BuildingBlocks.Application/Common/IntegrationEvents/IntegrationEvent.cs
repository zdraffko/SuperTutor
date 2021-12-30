namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Common.IntegrationEvents;

public abstract class IntegrationEvent
{
    protected IntegrationEvent() => Id = Guid.NewGuid();

    protected IntegrationEvent(Guid id) => Id = id;

    public Guid Id { get; }
}
