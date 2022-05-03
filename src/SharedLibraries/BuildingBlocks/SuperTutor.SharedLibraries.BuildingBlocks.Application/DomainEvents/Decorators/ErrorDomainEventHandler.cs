using Microsoft.Extensions.Logging;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents.Decorators;

public class ErrorDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
    private readonly ILogger<ErrorDomainEventHandler<TDomainEvent>> logger;
    private readonly IDomainEventHandler<TDomainEvent> decoratedDomainEventHandler;

    public ErrorDomainEventHandler(ILogger<ErrorDomainEventHandler<TDomainEvent>> logger, IDomainEventHandler<TDomainEvent> decoratedDomainEventHandler)
    {
        this.logger = logger;
        this.decoratedDomainEventHandler = decoratedDomainEventHandler;
    }

    public async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        try
        {
            await decoratedDomainEventHandler.Handle(domainEvent, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "The handler for domain event {@DomainEvent} threw an unexpected exception with message {ErrorMessage}", domainEvent, exception.Message);
        }
    }
}
