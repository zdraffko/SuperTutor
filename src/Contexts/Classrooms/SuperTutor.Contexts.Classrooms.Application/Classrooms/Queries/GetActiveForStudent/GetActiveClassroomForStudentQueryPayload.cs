using SuperTutor.Contexts.Classrooms.Domain.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForStudent;

public class GetActiveClassroomForStudentQueryPayload
{
    public GetActiveClassroomForStudentQueryPayload(ClassroomId? classroomId) => ClassroomId = classroomId;

    public ClassroomId? ClassroomId { get; }
}
