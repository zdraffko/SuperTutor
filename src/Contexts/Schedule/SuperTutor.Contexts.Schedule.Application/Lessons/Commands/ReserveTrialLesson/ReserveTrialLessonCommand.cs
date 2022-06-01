using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.ReserveTrialLesson;

public class ReserveTrialLessonCommand : Command<ReserveTrialLessonCommandPayload>
{
    public ReserveTrialLessonCommand(
        TutorId tutorId,
        StudentId studentId,
        DateOnly date,
        TimeOnly startTime,
        string subject,
        string grade)
    {
        TutorId = tutorId;
        StudentId = studentId;
        Date = date;
        StartTime = startTime;
        Subject = subject;
        Grade = grade;
    }

    public TutorId TutorId { get; }

    public StudentId StudentId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public string Subject { get; }

    public string Grade { get; }
}
