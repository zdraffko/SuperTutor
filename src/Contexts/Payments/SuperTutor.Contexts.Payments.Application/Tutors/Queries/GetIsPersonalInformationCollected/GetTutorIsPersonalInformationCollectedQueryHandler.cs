using FluentResults;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsPersonalInformationCollected;

internal class GetTutorIsPersonalInformationCollectedQueryHandler : IQueryHandler<GetTutorIsPersonalInformationCollectedQuery, GetTutorIsPersonalInformationCollectedQueryPayload>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public GetTutorIsPersonalInformationCollectedQueryHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task<Result<GetTutorIsPersonalInformationCollectedQueryPayload>> Handle(GetTutorIsPersonalInformationCollectedQuery query, CancellationToken cancellationToken)
    {
        var isPersonalInformationCollected = await tutorQueryModelRepository.GetIsPersonalInformationCollected(query.TutorId, cancellationToken);

        var queryPayload = new GetTutorIsPersonalInformationCollectedQueryPayload(isPersonalInformationCollected);

        return Result.Ok(queryPayload);
    }
}
