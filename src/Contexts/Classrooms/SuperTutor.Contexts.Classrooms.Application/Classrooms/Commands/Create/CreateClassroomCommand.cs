using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Create;

public class CreateClassroomCommand : Command
{
    public CreateClassroomCommand(string classroomName, TutorId tutorId)
    {
        ClassroomName = classroomName;
        TutorId = tutorId;
    }

    public string ClassroomName { get; }

    public TutorId TutorId { get; }
}
