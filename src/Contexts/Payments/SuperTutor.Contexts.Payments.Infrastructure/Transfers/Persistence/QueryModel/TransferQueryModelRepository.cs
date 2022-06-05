using SuperTutor.Contexts.Payments.Application.Transfers.Queries;
using SuperTutor.Contexts.Payments.Infrastructure.Shared.Persistence;

namespace SuperTutor.Contexts.Payments.Infrastructure.Transfers.Persistence.QueryModel;

internal class TransferQueryModelRepository : ITransferQueryModelRepository
{
    private readonly PaymentsDbContext paymentsDbContext;

    public TransferQueryModelRepository(PaymentsDbContext paymentsDbContext) => this.paymentsDbContext = paymentsDbContext;

    public async Task Create(TransferQueryModel transferQueryModel, CancellationToken cancellationToken)
    {
        paymentsDbContext.Transfers.Add(transferQueryModel);

        await paymentsDbContext.SaveChangesAsync(cancellationToken);
    }
}
