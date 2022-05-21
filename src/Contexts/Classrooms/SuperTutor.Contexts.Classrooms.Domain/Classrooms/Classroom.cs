using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Invariants;
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

    // TODO: Refactor the student related properties to a owned value object
    public StudentId? StudentId { get; private set; }

    public string? StudentConnectionId { get; private set; }

    public string? NotebookContent { get; private set; }

    public string? WhiteboardContent { get; private set; }

    public bool IsActive { get; private set; }

    public void Join(StudentId studentId, string studentConnectionId)
    {
        CheckInvariant(new ClassroomCanOnlyBeJoinedWhenItIsActiveInvariant(IsActive));

        StudentId = studentId;
        StudentConnectionId = studentConnectionId;
    }

    public void Close() => IsActive = false;

    public void Leave()
    {
        StudentId = null;
        StudentConnectionId = null;
    }

    public void SaveNotebookContent(string notebookContent) => NotebookContent = notebookContent;

    public void SaveWhiteboardContent(string whiteboardContent) => WhiteboardContent = whiteboardContent;
}
