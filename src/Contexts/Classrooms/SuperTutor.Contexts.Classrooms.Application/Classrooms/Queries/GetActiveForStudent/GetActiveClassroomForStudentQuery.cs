using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForStudent;

public class GetActiveClassroomForStudentQuery : Query<GetActiveClassroomForStudentQueryPayload>
{
    public GetActiveClassroomForStudentQuery(StudentId studentId) => StudentId = studentId;

    public StudentId StudentId { get; }
}
