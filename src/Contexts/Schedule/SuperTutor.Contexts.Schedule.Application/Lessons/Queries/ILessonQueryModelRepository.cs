using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries;

public interface ILessonQueryModelRepository
{
    Task Create(LessonQueryModel lessonQueryModel, CancellationToken cancellationToken);

    Task<IEnumerable<LessonId>> GetStartingLessonsIds(CancellationToken cancellationToken);

    Task SetAsScheduled(LessonId lessonId, LessonStatus status, LessonPaymentStatus paymentStatus, CancellationToken cancellationToken);
}
