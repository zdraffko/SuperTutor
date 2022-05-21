namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.Shared;

public interface IClassroomQueryRepository
{
    Task<string?> GetStudentConnectionId(string classroomName);
}
