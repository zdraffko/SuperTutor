using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForStudent;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Infrastructure.Shared.Persistence;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Lessons.Persistence.QueryModels;

internal class LessonQueryModelRepository : ILessonQueryModelRepository
{
    private readonly ScheduleDbContext scheduleDbContext;

    public LessonQueryModelRepository(ScheduleDbContext scheduleDbContext) => this.scheduleDbContext = scheduleDbContext;

    public async Task Create(LessonQueryModel lessonQueryModel, CancellationToken cancellationToken)
    {
        scheduleDbContext.Lessons.Add(lessonQueryModel);

        await scheduleDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<LessonId>> GetStartingLessonsIds(CancellationToken cancellationToken)
        => await scheduleDbContext.Lessons
            .Where(lesson
                => lesson.Status == LessonStatus.Scheduled.Name
                && lesson.Date >= DateTime.UtcNow.AddHours(3) && lesson.Date <= DateTime.UtcNow.AddHours(3).AddMinutes(5)) // TODO - Fix the timezones everywhere, this is a major problem for the entire system. The system should work with UTC dates only.
            .Select(lesson => lesson.Id)
            .ToListAsync(cancellationToken: cancellationToken);

    public async Task<IEnumerable<LessonId>> GetEndingLessonsIds(CancellationToken cancellationToken)
        => await scheduleDbContext.Lessons
            .Where(lesson
                => lesson.Status == LessonStatus.Started.Name
                && lesson.Date.AddHours(1) <= DateTime.UtcNow.AddHours(3)) // TODO - Fix the timezones, Fix hardcoded lesson duration
            .Select(lesson => lesson.Id)
            .ToListAsync(cancellationToken: cancellationToken);

    public async Task<IEnumerable<GetScheduledLessonsForStudentQueryPayload.ScheduledLesson>> GetScheduledLessonsForStudent(GetScheduledLessonsForStudentQuery query, CancellationToken cancellationToken)
    {
        var databaseQueryResult = await scheduleDbContext.Lessons
            .AsNoTracking()
            //.Where(lesson => lesson.StudentId == query.StudentId && lesson.Status == LessonStatus.Scheduled.Name && lesson.Date >= DateTime.UtcNow.AddHours(3)) TODO - Refactor this. This is just a dirty quick way to get the required functionality working on time
            .Where(lesson => lesson.StudentId == query.StudentId)
            .ToListAsync();

        return databaseQueryResult.Select(lesson => new GetScheduledLessonsForStudentQueryPayload.ScheduledLesson(
                lesson.Id,
                lesson.TutorId,
                lesson.StudentId,
                DateOnly.FromDateTime(lesson.Date),
                TimeOnly.FromTimeSpan(lesson.StartTime),
                lesson.Duration,
                lesson.Subject,
                lesson.Grade,
                lesson.Type,
                lesson.Status,
                lesson.PaymentStatus
            ));
    }

    public async Task SetAsScheduled(LessonId lessonId, LessonStatus status, LessonPaymentStatus paymentStatus, CancellationToken cancellationToken)
    {
        var updatedLessonQueryModel = new LessonQueryModel
        {
            Id = lessonId,
            Status = status.Name,
            PaymentStatus = paymentStatus.Name
        };

        scheduleDbContext.Attach(updatedLessonQueryModel);
        scheduleDbContext.Entry(updatedLessonQueryModel).Property(lessonQueryModel => lessonQueryModel.Status).IsModified = true;
        scheduleDbContext.Entry(updatedLessonQueryModel).Property(lessonQueryModel => lessonQueryModel.PaymentStatus).IsModified = true;

        await scheduleDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetAsStarted(LessonId lessonId, LessonStatus status, CancellationToken cancellationToken)
    {
        var updatedLessonQueryModel = new LessonQueryModel
        {
            Id = lessonId,
            Status = status.Name,
        };

        scheduleDbContext.Attach(updatedLessonQueryModel);
        scheduleDbContext.Entry(updatedLessonQueryModel).Property(lessonQueryModel => lessonQueryModel.Status).IsModified = true;

        await scheduleDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetAsEnded(LessonId lessonId, LessonStatus status, CancellationToken cancellationToken)
    {
        var updatedLessonQueryModel = new LessonQueryModel
        {
            Id = lessonId,
            Status = status.Name,
        };

        scheduleDbContext.Attach(updatedLessonQueryModel);
        scheduleDbContext.Entry(updatedLessonQueryModel).Property(lessonQueryModel => lessonQueryModel.Status).IsModified = true;

        await scheduleDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetAsCompleted(LessonId lessonId, LessonStatus status, CancellationToken cancellationToken)
    {
        var updatedLessonQueryModel = new LessonQueryModel
        {
            Id = lessonId,
            Status = status.Name,
        };

        scheduleDbContext.Attach(updatedLessonQueryModel);
        scheduleDbContext.Entry(updatedLessonQueryModel).Property(lessonQueryModel => lessonQueryModel.Status).IsModified = true;

        await scheduleDbContext.SaveChangesAsync(cancellationToken);
    }
}
