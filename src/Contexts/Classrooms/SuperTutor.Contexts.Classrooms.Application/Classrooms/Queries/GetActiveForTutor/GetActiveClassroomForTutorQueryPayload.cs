using SuperTutor.Contexts.Classrooms.Domain.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForTutor;

public class GetActiveClassroomForTutorQueryPayload
{
    public GetActiveClassroomForTutorQueryPayload(ClassroomId? classroomId) => ClassroomId = classroomId;

    public ClassroomId? ClassroomId { get; }
}
