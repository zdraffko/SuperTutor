namespace SuperTutor.ApiGateways.Web.Models.Catalog.ReserveTrialLesson;

public class ReserveTrialLessonRequest
{
    public ReserveTrialLessonRequest(
        Guid tutorId,
        DateOnly date,
        TimeOnly startTime,
        string subject,
        string grade)
    {
        TutorId = tutorId;
        Date = date;
        StartTime = startTime;
        Subject = subject;
        Grade = grade;
    }

    public Guid TutorId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public string Subject { get; }

    public string Grade { get; }
}
