using SuperTutor.SharedLibraries.BuildingBlocks.Application.Common.IntegrationEvents;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Contracts;

public interface IIntegrationEventsService
{
    void Raise(IntegrationEvent integrationEvent);

    Task DispatchAll(CancellationToken cancellationToken);
}
