using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreTermsOfServiceAccepted;

public class GetTutorAreTermsOfServiceAcceptedQuery : Query<GetTutorAreTermsOfServiceAcceptedQueryPayload>
{
    public GetTutorAreTermsOfServiceAcceptedQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
