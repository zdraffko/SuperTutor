using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.End;

public class EndLessonCommand : Command
{
    public EndLessonCommand(LessonId lessonId) => LessonId = lessonId;

    public LessonId LessonId { get; }
}
