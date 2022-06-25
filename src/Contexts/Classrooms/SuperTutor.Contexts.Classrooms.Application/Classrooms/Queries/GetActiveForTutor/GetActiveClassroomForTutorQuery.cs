using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForTutor;

public class GetActiveClassroomForTutorQuery : Query<GetActiveClassroomForTutorQueryPayload>
{
    public GetActiveClassroomForTutorQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
