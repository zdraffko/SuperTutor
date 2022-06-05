using SuperTutor.Contexts.Payments.Application.Transfers.Queries;
using SuperTutor.Contexts.Payments.Domain.Transfers.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Payments.Application.Transfers.DomainEvents.TransferCreated;

internal class CreateTransferQueryModelDomainEventHandler : IDomainEventHandler<TransferCreatedDomainEvent>
{
    private readonly ITransferQueryModelRepository transferQueryModelRepository;

    public CreateTransferQueryModelDomainEventHandler(ITransferQueryModelRepository transferQueryModelRepository) => this.transferQueryModelRepository = transferQueryModelRepository;

    public async Task Handle(TransferCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var transferQueryModel = new TransferQueryModel
        {
            Id = domainEvent.TransferId,
            ChargeId = domainEvent.ChargeId,
            LessonId = domainEvent.LessonId,
            StudentId = domainEvent.StudentId,
            TutorId = domainEvent.TutorId,
            Amount = domainEvent.Amount,
            Currency = domainEvent.Currency
        };

        await transferQueryModelRepository.Create(transferQueryModel, cancellationToken);
    }
}
