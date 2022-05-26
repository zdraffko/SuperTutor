using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsBankAccountInformationCollected;

public class GetTutorIsBankAccountInformationCollectedQuery : Query<GetTutorIsBankAccountInformationCollectedQueryPayload>
{
    public GetTutorIsBankAccountInformationCollectedQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
