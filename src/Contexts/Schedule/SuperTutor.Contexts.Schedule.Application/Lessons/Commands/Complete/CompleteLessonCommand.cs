using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.Complete;

public class CompleteLessonCommand : Command
{
    public CompleteLessonCommand(LessonId lessonId) => LessonId = lessonId;

    public LessonId LessonId { get; }
}
