using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Invariants;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons;

public class Lesson : AggregateRoot<LessonId, Guid>
{
    private static readonly TimeSpan TrialLessonDuration = TimeSpan.FromHours(1);

    // Required for loading the aggregate root from the event store 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Lesson() : base(new LessonId(Guid.Empty)) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Lesson(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        TimeSpan duration,
        string subject,
        string grade,
        LessonType type) : base(new LessonId(Guid.NewGuid()))
    {
        TutorId = tutorId;
        StudentId = studentId;
        Date = date;
        StartTime = startTime;
        Duration = duration;
        Subject = subject;
        Grade = grade;
        Type = type;
        Status = LessonStatus.Reserved;
        PaymentStatus = LessonPaymentStatus.WaitingPayment;
    }

    public TutorId TutorId { get; private set; }

    public StudentId StudentId { get; private set; }

    public DateOnly Date { get; private set; }

    public TimeOnly StartTime { get; private set; }

    public TimeSpan Duration { get; private set; }

    public string Subject { get; private set; }

    public string Grade { get; private set; }

    public LessonType Type { get; private set; }

    public LessonStatus Status { get; private set; }

    public LessonPaymentStatus PaymentStatus { get; private set; }

    public PaymentId? PaymentId { get; private set; }

    public static Lesson ReserveTrialLesson(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        string subject,
        string grade)
    {
        var trialLesson = new Lesson(
            tutorId,
            studentId,
            date,
            startTime,
            TrialLessonDuration,
            subject,
            grade,
            LessonType.Trial);

        trialLesson.CheckInvariant(new LessonDateAndTimeMustBeIntoTheFutureInvariant(trialLesson.Date, trialLesson.StartTime));

        trialLesson.RaiseDomainEvent(new LessonReservedDomainEvent(
            trialLesson.Id,
            trialLesson.TutorId,
            trialLesson.StudentId,
            trialLesson.Date,
            trialLesson.StartTime,
            trialLesson.Duration,
            trialLesson.Subject,
            trialLesson.Grade,
            trialLesson.Type,
            trialLesson.Status,
            trialLesson.PaymentStatus));

        return trialLesson;
    }

    public static Lesson ReserveRegularLesson(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        TimeSpan duration,
        string subject,
        string grade) => new(tutorId, studentId, date, startTime, duration, subject, grade, LessonType.Regular);

    public void Schedule(PaymentId paymentId)
    {
        PaymentId = paymentId;
        PaymentStatus = LessonPaymentStatus.Paid;
        Status = LessonStatus.Scheduled;

        RaiseDomainEvent(new LessonScheduledDomainEvent(Id, PaymentId, PaymentStatus, Status));
    }

    public void Start()
    {
        Status = LessonStatus.Started;

        RaiseDomainEvent(new LessonStartedDomainEvent(Id, Status));
    }

    #region Apply Domain Events

    public override void ApplyDomainEvent(DomainEvent domainEvent) => Apply((dynamic) domainEvent);

    private void Apply(LessonReservedDomainEvent domainEvent)
    {
        Id = domainEvent.LessonId;
        TutorId = domainEvent.TutorId;
        StudentId = domainEvent.StudentId;
        Date = domainEvent.Date;
        StartTime = domainEvent.StartTime;
        Duration = domainEvent.Duration;
        Subject = domainEvent.Subject;
        Grade = domainEvent.Grade;
        Type = domainEvent.Type;
        Status = domainEvent.Status;
        PaymentStatus = domainEvent.PaymentStatus;
    }

    private void Apply(LessonScheduledDomainEvent domainEvent)
    {
        Id = domainEvent.LessonId;
        Status = domainEvent.Status;
        PaymentStatus = domainEvent.PaymentStatus;
        PaymentId = domainEvent.PaymentId;
    }

    private void Apply(LessonStartedDomainEvent domainEvent)
    {
        Id = domainEvent.LessonId;
        Status = domainEvent.Status;
    }

    #endregion Apply Domain Events
}
