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

    public Task<IEnumerable<LessonId>> GetStartingLessonsIds(CancellationToken cancellationToken) => throw new NotImplementedException();

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
}
