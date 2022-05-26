using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsPersonalInformationCollected;

public class GetTutorIsPersonalInformationCollectedQuery : Query<GetTutorIsPersonalInformationCollectedQueryPayload>
{
    public GetTutorIsPersonalInformationCollectedQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
