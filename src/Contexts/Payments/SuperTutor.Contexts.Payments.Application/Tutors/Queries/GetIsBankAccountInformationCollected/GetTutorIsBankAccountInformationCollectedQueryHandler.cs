using FluentResults;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsBankAccountInformationCollected;

internal class GetTutorIsBankAccountInformationCollectedQueryHandler : IQueryHandler<GetTutorIsBankAccountInformationCollectedQuery, GetTutorIsBankAccountInformationCollectedQueryPayload>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public GetTutorIsBankAccountInformationCollectedQueryHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task<Result<GetTutorIsBankAccountInformationCollectedQueryPayload>> Handle(GetTutorIsBankAccountInformationCollectedQuery query, CancellationToken cancellationToken)
    {
        var isBankAccountInformationCollected = await tutorQueryModelRepository.GetIsBankAccountInformationCollected(query.TutorId, cancellationToken);

        var queryPayload = new GetTutorIsBankAccountInformationCollectedQueryPayload(isBankAccountInformationCollected);

        return Result.Ok(queryPayload);
    }
}
