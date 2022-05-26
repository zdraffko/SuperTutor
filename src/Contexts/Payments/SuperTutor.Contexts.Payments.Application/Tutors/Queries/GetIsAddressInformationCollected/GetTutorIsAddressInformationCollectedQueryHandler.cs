using FluentResults;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsAddressInformationCollected;

internal class GetTutorIsAddressInformationCollectedQueryHandler : IQueryHandler<GetTutorIsAddressInformationCollectedQuery, GetTutorIsAddressInformationCollectedQueryPayload>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public GetTutorIsAddressInformationCollectedQueryHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task<Result<GetTutorIsAddressInformationCollectedQueryPayload>> Handle(GetTutorIsAddressInformationCollectedQuery query, CancellationToken cancellationToken)
    {
        var isAddressInformationCollected = await tutorQueryModelRepository.GetIsAddressInformationCollected(query.TutorId, cancellationToken);

        var queryPayload = new GetTutorIsAddressInformationCollectedQueryPayload(isAddressInformationCollected);

        return Result.Ok(queryPayload);
    }
}
