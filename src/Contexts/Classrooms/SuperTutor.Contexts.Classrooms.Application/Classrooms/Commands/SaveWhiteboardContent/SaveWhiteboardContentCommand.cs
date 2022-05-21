using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveWhiteboardContent;

public class SaveWhiteboardContentCommand : Command
{
    public SaveWhiteboardContentCommand(string classroomName, string whiteboardContent)
    {
        ClassroomName = classroomName;
        WhiteboardContent = whiteboardContent;
    }

    public string ClassroomName { get; }

    public string WhiteboardContent { get; }
}
