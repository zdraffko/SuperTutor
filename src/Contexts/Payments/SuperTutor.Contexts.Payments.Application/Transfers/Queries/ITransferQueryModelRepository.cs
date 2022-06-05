namespace SuperTutor.Contexts.Payments.Application.Transfers.Queries;

public interface ITransferQueryModelRepository
{
    Task Create(TransferQueryModel transferQueryModel, CancellationToken cancellationToken);
}
