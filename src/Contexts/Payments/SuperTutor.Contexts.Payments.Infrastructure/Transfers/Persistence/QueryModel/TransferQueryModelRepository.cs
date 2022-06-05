using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Payments.Application.Transfers.Queries;
using SuperTutor.Contexts.Payments.Application.Transfers.Queries.GetForTutor;
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

    public async Task<IEnumerable<GetTransfersForTutorQueryPayload.Transfer>> GetTransfersForTutor(GetTransfersForTutorQuery query, CancellationToken cancellationToken)
        => await paymentsDbContext.Transfers
            .Where(transfer => transfer.TutorId == query.TutorId)
            .Select(transfer => new GetTransfersForTutorQueryPayload.Transfer
            {
                Id = transfer.Id,
                ChargeId = transfer.ChargeId,
                LessonId = transfer.LessonId,
                StudentId = transfer.StudentId,
                TutorId = transfer.TutorId,
                Amount = transfer.Amount,
                Currency = transfer.Currency
            })
            .ToListAsync(cancellationToken);

}
