namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetStudentConnectionId;

public class GetClassroomStudentConnectionIdQueryPayload
{
    public GetClassroomStudentConnectionIdQueryPayload(string? studentConnectionId) => StudentConnectionId = studentConnectionId;

    public string? StudentConnectionId { get; }
}
