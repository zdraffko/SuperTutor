using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Application.Transfers.Queries.GetForTutor;

public class GetTransfersForTutorQuery : Query<GetTransfersForTutorQueryPayload>
{
    public GetTransfersForTutorQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
