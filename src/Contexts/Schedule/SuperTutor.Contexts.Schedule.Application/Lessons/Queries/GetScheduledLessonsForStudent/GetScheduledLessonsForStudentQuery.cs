using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForStudent;

public class GetScheduledLessonsForStudentQuery : Query<GetScheduledLessonsForStudentQueryPayload>
{
    public GetScheduledLessonsForStudentQuery(StudentId studentId) => StudentId = studentId;

    public StudentId StudentId { get; }
}
