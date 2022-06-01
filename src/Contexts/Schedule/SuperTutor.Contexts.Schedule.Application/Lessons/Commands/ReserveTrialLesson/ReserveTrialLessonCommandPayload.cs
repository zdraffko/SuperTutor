using SuperTutor.Contexts.Schedule.Domain.Lessons;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.ReserveTrialLesson;

public class ReserveTrialLessonCommandPayload
{
    public ReserveTrialLessonCommandPayload(LessonId lessonId) => LessonId = lessonId;

    public LessonId LessonId { get; }
}
