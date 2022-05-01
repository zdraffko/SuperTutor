using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Invariants;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons;

public class Lesson : AggregateRoot<LessonId, Guid>
{
    private static readonly TimeSpan TrialLessonDuration = TimeSpan.FromHours(1);

    private Lesson(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        TimeSpan duration,
        IEnumerable<TimeSlotId> timeSlotIds,
        string subject,
        string grade,
        LessonType type) : base(new LessonId(Guid.NewGuid()))
    {
        TutorId = tutorId;
        StudentId = studentId;
        Date = date;
        StartTime = startTime;
        Duration = duration;
        TimeSlotIds = timeSlotIds;
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

    public IEnumerable<TimeSlotId> TimeSlotIds { get; private set; }

    public string Subject { get; private set; }

    public string Grade { get; private set; }

    public LessonType Type { get; private set; }

    public LessonStatus Status { get; private set; }

    public LessonPaymentStatus PaymentStatus { get; private set; }

    public static Lesson ReserveTrialLesson(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        IEnumerable<TimeSlotId> timeSlotIds,
        string subject,
        string grade)
    {
        var trialLesson = new Lesson(
            tutorId,
            studentId,
            date,
            startTime,
            TrialLessonDuration,
            timeSlotIds,
            subject,
            grade,
            LessonType.Trial);

        trialLesson.CheckInvariant(new LessonDateAndTimeMustBeIntoTheFutureInvariant(trialLesson.Date, trialLesson.StartTime));

        trialLesson.RaiseDomainEvent(new TrialLessonReservedDomainEvent(
            trialLesson.Id,
            trialLesson.TutorId,
            trialLesson.StudentId,
            trialLesson.Date,
            trialLesson.StartTime,
            trialLesson.TimeSlotIds,
            trialLesson.Subject,
            trialLesson.Grade));

        return trialLesson;
    }

    public static Lesson ReserveRegularLesson(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        TimeSpan duration,
        IEnumerable<TimeSlotId> timeSlotIds,
        string subject,
        string grade) => new(tutorId, studentId, date, startTime, duration, timeSlotIds, subject, grade, LessonType.Regular);

    #region Apply Domain Events

    public override void ApplyDomainEvent(DomainEvent domainEvent) => Apply((dynamic) domainEvent);

    private void Apply(TrialLessonReservedDomainEvent domainEvent)
    {
        Id = domainEvent.LessonId;
        TutorId = domainEvent.TutorId;
        StudentId = domainEvent.StudentId;
        Date = domainEvent.Date;
        StartTime = domainEvent.StartTime;
        Duration = TrialLessonDuration;
        TimeSlotIds = domainEvent.TimeSlotIds;
        Subject = domainEvent.Subject;
        Grade = domainEvent.Grade;
        Type = LessonType.Trial;
        Status = LessonStatus.Reserved;
        PaymentStatus = LessonPaymentStatus.WaitingPayment;
    }

    #endregion Apply Domain Events
}
