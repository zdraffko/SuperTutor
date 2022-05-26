using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsAddressInformationCollected;

public class GetTutorIsAddressInformationCollectedQuery : Query<GetTutorIsAddressInformationCollectedQueryPayload>
{
    public GetTutorIsAddressInformationCollectedQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
