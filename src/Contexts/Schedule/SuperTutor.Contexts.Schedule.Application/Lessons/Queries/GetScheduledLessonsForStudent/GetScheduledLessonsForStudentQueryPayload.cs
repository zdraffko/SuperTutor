using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForStudent;

public class GetScheduledLessonsForStudentQueryPayload
{
    public GetScheduledLessonsForStudentQueryPayload(IEnumerable<ScheduledLesson> scheduledLessons) => ScheduledLessons = scheduledLessons;

    public IEnumerable<ScheduledLesson> ScheduledLessons { get; }

    public class ScheduledLesson
    {
        public ScheduledLesson(
            LessonId id,
            TutorId tutorId,
            StudentId studentId,
            DateOnly date,
            TimeOnly startTime,
            TimeSpan duration,
            string subject,
            string grade,
            string type,
            string status,
            string paymentStatus)
        {
            Id = id;
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

        public LessonId Id { get; }

        public TutorId TutorId { get; }

        public StudentId StudentId { get; }

        public DateOnly Date { get; }

        public TimeOnly StartTime { get; }

        public TimeSpan Duration { get; }

        public string Subject { get; }

        public string Grade { get; }

        public string Type { get; }

        public string Status { get; }

        public string PaymentStatus { get; }
    }
}
