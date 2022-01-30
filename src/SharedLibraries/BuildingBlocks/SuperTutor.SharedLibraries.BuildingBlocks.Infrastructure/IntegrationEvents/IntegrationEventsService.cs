using MassTransit;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.IntegrationEvents;

public class IntegrationEventsService : IIntegrationEventsService
{
    private readonly List<IntegrationEvent> integrationEvents;
    private readonly IPublishEndpoint publishEndpoint;

    public IntegrationEventsService(IPublishEndpoint publishEndpoint)
    {
        integrationEvents = new List<IntegrationEvent>();
        this.publishEndpoint = publishEndpoint;
    }

    public void Raise(IntegrationEvent integrationEvent) => integrationEvents.Add(integrationEvent);

    public async Task DispatchAll(CancellationToken cancellationToken)
    {
        foreach (var integrationEvent in integrationEvents)
        {
            await publishEndpoint.Publish(integrationEvent, integrationEvent.GetType(), cancellationToken);
        }
    }
}
