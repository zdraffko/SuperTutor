using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.Abandon;

public class AbandonLessonCommand : Command
{
    public AbandonLessonCommand(LessonId lessonId) => LessonId = lessonId;

    public LessonId LessonId { get; }
}
