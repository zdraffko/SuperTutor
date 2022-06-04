using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Events;

public class LessonReservedDomainEvent : DomainEvent
{
    public LessonReservedDomainEvent(
        LessonId lessonId,
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        TimeSpan duration,
        string subject,
        string grade,
        LessonType type,
        LessonStatus status,
        LessonPaymentStatus paymentStatus)
    {
        LessonId = lessonId;
        TutorId = tutorId;
        StudentId = studentId;
        Date = date;
        StartTime = startTime;
        Duration = duration;
        Subject = subject;
        Grade = grade;
        Type = type;
        Status = status;
        PaymentStatus = paymentStatus;
    }

    public LessonId LessonId { get; }

    public TutorId TutorId { get; }

    public StudentId StudentId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public TimeSpan Duration { get; }

    public string Subject { get; }

    public string Grade { get; }

    public LessonType Type { get; }

    public LessonStatus Status { get; }

    public LessonPaymentStatus PaymentStatus { get; }
}
