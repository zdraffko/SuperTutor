using SuperTutor.Contexts.Payments.Application.Transfers.Queries.GetForTutor;

namespace SuperTutor.Contexts.Payments.Application.Transfers.Queries;

public interface ITransferQueryModelRepository
{
    Task Create(TransferQueryModel transferQueryModel, CancellationToken cancellationToken);

    Task<IEnumerable<GetTransfersForTutorQueryPayload.Transfer>> GetTransfersForTutor(GetTransfersForTutorQuery query, CancellationToken cancellationToken);
}
