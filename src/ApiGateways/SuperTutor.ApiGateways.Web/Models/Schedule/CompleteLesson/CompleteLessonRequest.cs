namespace SuperTutor.ApiGateways.Web.Models.Schedule.CompleteLesson;

public class CompleteLessonRequest
{
    public CompleteLessonRequest(Guid lessonId) => LessonId = lessonId;

    public Guid LessonId { get; }
}
