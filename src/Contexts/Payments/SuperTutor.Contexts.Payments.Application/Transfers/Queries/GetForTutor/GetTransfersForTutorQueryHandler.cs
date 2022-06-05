using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Transfers.Queries.GetForTutor;

internal class GetTransfersForTutorQueryHandler : IQueryHandler<GetTransfersForTutorQuery, GetTransfersForTutorQueryPayload>
{
    private readonly ITransferQueryModelRepository transferQueryModelRepository;

    public GetTransfersForTutorQueryHandler(ITransferQueryModelRepository transferQueryModelRepository) => this.transferQueryModelRepository = transferQueryModelRepository;

    public async Task<Result<GetTransfersForTutorQueryPayload>> Handle(GetTransfersForTutorQuery query, CancellationToken cancellationToken)
    {
        var transfers = await transferQueryModelRepository.GetTransfersForTutor(query, cancellationToken);
        var payload = new GetTransfersForTutorQueryPayload(transfers);

        return Result.Ok(payload);
    }
}
