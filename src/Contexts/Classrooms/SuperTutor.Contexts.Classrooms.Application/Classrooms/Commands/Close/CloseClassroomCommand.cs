using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Close;

public class CloseClassroomCommand : Command
{
    public CloseClassroomCommand(LessonId lessonId) => LessonId = lessonId;

    public LessonId LessonId { get; }
}
