using FluentResults;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreVerificationDocumentsCollected;

internal class GetTutorAreVerificationDocumentsCollectedQueryHandler : IQueryHandler<GetTutorAreVerificationDocumentsCollectedQuery, GetTutorAreVerificationDocumentsCollectedQueryPayload>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public GetTutorAreVerificationDocumentsCollectedQueryHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task<Result<GetTutorAreVerificationDocumentsCollectedQueryPayload>> Handle(GetTutorAreVerificationDocumentsCollectedQuery query, CancellationToken cancellationToken)
    {
        var areVerificationDocumentsCollected = await tutorQueryModelRepository.GetAreVerificationDocumentsCollected(query.TutorId, cancellationToken);

        var queryPayload = new GetTutorAreVerificationDocumentsCollectedQueryPayload(areVerificationDocumentsCollected);

        return Result.Ok(queryPayload);
    }
}
