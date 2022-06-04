using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
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
                && lesson.Date >= DateTime.UtcNow.AddHours(3) && lesson.Date <= DateTime.UtcNow.AddHours(3).AddMinutes(5)) // TODO - Fix the timezones
            .Select(lesson => lesson.Id)
            .ToListAsync(cancellationToken: cancellationToken);

    public async Task<IEnumerable<LessonId>> GetEndingLessonsIds(CancellationToken cancellationToken)
        => await scheduleDbContext.Lessons
            .Where(lesson
                => lesson.Status == LessonStatus.Scheduled.Name
                && lesson.Date <= DateTime.UtcNow.AddHours(3 + 1)) // TODO - Fix the timezones, Fix hardcoded lesson duration
            .Select(lesson => lesson.Id)
            .ToListAsync(cancellationToken: cancellationToken);

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
}
