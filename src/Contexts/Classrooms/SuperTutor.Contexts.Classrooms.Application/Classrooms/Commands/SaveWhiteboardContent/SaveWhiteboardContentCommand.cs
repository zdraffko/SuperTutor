using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveWhiteboardContent;

public class SaveWhiteboardContentCommand : Command
{
    public SaveWhiteboardContentCommand(ClassroomId classroomId, string whiteboardContent)
    {
        ClassroomId = classroomId;
        WhiteboardContent = whiteboardContent;
    }

    public ClassroomId ClassroomId { get; }

    public string WhiteboardContent { get; }
}
