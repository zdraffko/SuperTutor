using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;

namespace SuperTutor.Contexts.Classrooms.Domain.Classrooms;

public class Classroom : Entity<ClassroomId, Guid>, IAggregateRoot
{
    public Classroom(string name, TutorId tutorId) : base(new ClassroomId(Guid.NewGuid()))
    {
        Name = name;
        TutorId = tutorId;
        IsActive = true;
    }

    public string Name { get; }

    public TutorId TutorId { get; }

    public StudentId? StudentId { get; }

    public string? NotebookContent { get; }

    public string? WhiteboardContent { get; }

    public bool IsActive { get; }
}
