using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.DomainEvents;

internal class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DomainEventDispatcher> logger;

    public DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public async Task Dispatch<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken) where TDomainEvent : DomainEvent
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var domainEventHandlers = GetDomainEventHandlers<TDomainEvent>(scope);

        foreach (var domainEventHandler in domainEventHandlers)
        {
            await domainEventHandler.Handle(domainEvent, cancellationToken);
        }
    }

    private IEnumerable<IDomainEventHandler<TDomainEvent>> GetDomainEventHandlers<TDomainEvent>(AsyncServiceScope scope) where TDomainEvent : DomainEvent
    {
        try
        {
            return scope.ServiceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
        }
        catch (Exception exception)
        {
            logger.LogInformation(exception, "No domain event handlers are registered for '{DomainEventName}'", typeof(TDomainEvent).FullName);

            return Enumerable.Empty<IDomainEventHandler<TDomainEvent>>();
        }
    }
}
