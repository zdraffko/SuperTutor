using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForTutor;

public class GetScheduledLessonsForTutorQuery : Query<GetScheduledLessonsForTutorQueryPayload>
{
    public GetScheduledLessonsForTutorQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
