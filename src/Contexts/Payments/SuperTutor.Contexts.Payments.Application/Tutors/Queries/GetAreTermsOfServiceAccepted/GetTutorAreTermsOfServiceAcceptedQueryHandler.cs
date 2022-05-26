using FluentResults;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreTermsOfServiceAccepted;

internal class GetTutorIsBankAccountInformationCollectedQueryHandler : IQueryHandler<GetTutorAreTermsOfServiceAcceptedQuery, GetTutorAreTermsOfServiceAcceptedQueryPayload>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public GetTutorIsBankAccountInformationCollectedQueryHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task<Result<GetTutorAreTermsOfServiceAcceptedQueryPayload>> Handle(GetTutorAreTermsOfServiceAcceptedQuery query, CancellationToken cancellationToken)
    {
        var areTermsOfServiceAccepted = await tutorQueryModelRepository.GetAreTermsOfServiceAccepted(query.TutorId, cancellationToken);

        var queryPayload = new GetTutorAreTermsOfServiceAcceptedQueryPayload(areTermsOfServiceAccepted);

        return Result.Ok(queryPayload);
    }
}
