namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

public interface IIntegrationEventsService
{
    void Raise(IntegrationEvent integrationEvent);

    Task DispatchAll(CancellationToken cancellationToken);
}
