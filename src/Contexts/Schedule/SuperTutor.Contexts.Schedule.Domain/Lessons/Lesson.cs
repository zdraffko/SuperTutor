using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons;

public class Lesson : AggregateRoot<LessonId, Guid>
{
    private static readonly TimeSpan TrialLessonDuration = TimeSpan.FromHours(1);

    private Lesson(DateOnly date, TimeOnly startTime, TimeSpan duration, LessonType type) : base(new LessonId(Guid.NewGuid()))
    {
        Date = date;
        StartTime = startTime;
        Duration = duration;
        Type = type;
        Status = LessonStatus.Reserved;
        PaymentStatus = LessonPaymentStatus.WaitingPayment;
    }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public TimeSpan Duration { get; }

    public LessonType Type { get; }

    public LessonStatus Status { get; }

    public LessonPaymentStatus PaymentStatus { get; }

    public static Lesson ReserveTrialLesson(DateOnly date, TimeOnly startTime) => new(date, startTime, TrialLessonDuration, LessonType.Trial);

    public static Lesson ReserveRegularLesson(DateOnly date, TimeOnly startTime, TimeSpan duration) => new(date, startTime, duration, LessonType.Regular);

    protected override void ApplyDomainEvent(DomainEvent domainEvent) => throw new NotImplementedException();
}
