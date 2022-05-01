using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Events;

public class TrialLessonReservedDomainEvent : DomainEvent
{
    public TrialLessonReservedDomainEvent(
        LessonId lessonId,
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        IEnumerable<TimeSlotId> timeSlotIds,
        string subject,
        string grade)
    {
        LessonId = lessonId;
        TutorId = tutorId;
        StudentId = studentId;
        Date = date;
        StartTime = startTime;
        TimeSlotIds = timeSlotIds;
        Subject = subject;
        Grade = grade;
    }

    public LessonId LessonId { get; }

    public TutorId TutorId { get; }

    public StudentId StudentId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public IEnumerable<TimeSlotId> TimeSlotIds { get; }

    public string Subject { get; }

    public string Grade { get; }
}
