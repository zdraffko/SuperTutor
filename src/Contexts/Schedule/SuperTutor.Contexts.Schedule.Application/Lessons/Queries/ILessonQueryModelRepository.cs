using SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForStudent;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForTutor;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries;

public interface ILessonQueryModelRepository
{
    Task Create(LessonQueryModel lessonQueryModel, CancellationToken cancellationToken);

    Task<IEnumerable<LessonId>> GetStartingLessonsIds(CancellationToken cancellationToken);

    Task<IEnumerable<LessonId>> GetEndingLessonsIds(CancellationToken cancellationToken);

    Task<IEnumerable<GetScheduledLessonsForStudentQueryPayload.ScheduledLesson>> GetScheduledLessonsForStudent(GetScheduledLessonsForStudentQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<GetScheduledLessonsForTutorQueryPayload.ScheduledLesson>> GetScheduledLessonsForTutor(GetScheduledLessonsForTutorQuery query, CancellationToken cancellationToken);

    Task SetAsScheduled(LessonId lessonId, LessonStatus status, LessonPaymentStatus paymentStatus, CancellationToken cancellationToken);

    Task SetAsStarted(LessonId lessonId, LessonStatus status, CancellationToken cancellationToken);

    Task SetAsEnded(LessonId lessonId, LessonStatus status, CancellationToken cancellationToken);

    Task SetAsCompleted(LessonId lessonId, LessonStatus status, CancellationToken cancellationToken);
}
