using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Join;

public class JoinClassroomCommand : Command
{
    public JoinClassroomCommand(string classroomName, StudentId studentId, string studentConnectionId)
    {
        ClassroomName = classroomName;
        StudentId = studentId;
        StudentConnectionId = studentConnectionId;
    }

    public string ClassroomName { get; }

    public StudentId StudentId { get; }

    public string StudentConnectionId { get; }
}
