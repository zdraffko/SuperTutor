using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Close;

public class CloseClassroomCommand : Command
{
    public CloseClassroomCommand(string classroomName) => ClassroomName = classroomName;

    public string ClassroomName { get; }
}
