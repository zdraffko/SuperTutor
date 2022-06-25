using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveNotebookContent;

public class SaveNotebookContentCommand : Command
{
    public SaveNotebookContentCommand(ClassroomId classroomId, string notebookContent)
    {
        ClassroomId = classroomId;
        NotebookContent = notebookContent;
    }

    public ClassroomId ClassroomId { get; }

    public string NotebookContent { get; }
}
