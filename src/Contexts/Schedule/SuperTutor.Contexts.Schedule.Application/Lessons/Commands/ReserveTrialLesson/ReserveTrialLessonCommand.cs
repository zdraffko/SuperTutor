using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.ReserveTrialLesson;

public class ReserveTrialLessonCommand : Command
{
    public ReserveTrialLessonCommand(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        IEnumerable<TimeSlotId> timeSlotIds,
        string subject,
        string grade)
    {
        TutorId = tutorId;
        StudentId = studentId;
        Date = date;
        StartTime = startTime;
        TimeSlotIds = timeSlotIds;
        Subject = subject;
        Grade = grade;
    }

    public TutorId TutorId { get; }

    public StudentId StudentId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public IEnumerable<TimeSlotId> TimeSlotIds { get; }

    public string Subject { get; }

    public string Grade { get; }
}
