using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Commands.Start;

public class StartLessonCommand : Command
{
    public StartLessonCommand(LessonId lessonId) => LessonId = lessonId;

    public LessonId LessonId { get; }
}
