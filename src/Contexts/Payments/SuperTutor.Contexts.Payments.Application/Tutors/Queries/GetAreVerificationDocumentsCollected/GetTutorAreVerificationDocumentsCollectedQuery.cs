using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreVerificationDocumentsCollected;

public class GetTutorAreVerificationDocumentsCollectedQuery : Query<GetTutorAreVerificationDocumentsCollectedQueryPayload>
{
    public GetTutorAreVerificationDocumentsCollectedQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
