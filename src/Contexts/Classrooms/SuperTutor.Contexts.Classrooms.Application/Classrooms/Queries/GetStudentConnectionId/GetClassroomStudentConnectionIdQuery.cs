using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetStudentConnectionId;

public class GetClassroomStudentConnectionIdQuery : Query<GetClassroomStudentConnectionIdQueryPayload>
{
    public GetClassroomStudentConnectionIdQuery(string classroomName) => ClassroomName = classroomName;

    public string ClassroomName { get; }
}
