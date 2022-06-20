using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries;

public class LessonQueryModel
{
    public LessonId Id { get; init; }

    public TutorId TutorId { get; init; }

    public StudentId StudentId { get; init; }

    public DateTime DateOfReservation { get; init; }

    public DateTime Date { get; init; }

    public TimeSpan StartTime { get; init; }

    public TimeSpan Duration { get; init; }

    public string Subject { get; init; }

    public string Grade { get; init; }

    public string Type { get; init; }

    public string Status { get; init; }

    public string PaymentStatus { get; init; }
}
