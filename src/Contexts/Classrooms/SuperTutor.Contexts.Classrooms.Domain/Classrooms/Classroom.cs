using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;

namespace SuperTutor.Contexts.Classrooms.Domain.Classrooms;

public class Classroom : Entity<ClassroomId, Guid>, IAggregateRoot
{
    public Classroom(LessonId lessonId, TutorId tutorId, StudentId studentId) : base(new ClassroomId(Guid.NewGuid()))
    {
        LessonId = lessonId;
        TutorId = tutorId;
        StudentId = studentId;
        IsActive = true;
    }

    public LessonId LessonId { get; }

    public TutorId TutorId { get; }

    public StudentId StudentId { get; }

    public string? NotebookContent { get; private set; }

    public string? WhiteboardContent { get; private set; }

    public bool IsActive { get; private set; }

    public void Close() => IsActive = false;

    public void SaveNotebookContent(string notebookContent) => NotebookContent = notebookContent;

    public void SaveWhiteboardContent(string whiteboardContent) => WhiteboardContent = whiteboardContent;
}
