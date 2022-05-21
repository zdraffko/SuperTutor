using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Leave;

public class LeaveClassroomCommand : Command
{
    public LeaveClassroomCommand(string classroomName) => ClassroomName = classroomName;

    public string ClassroomName { get; }
}
