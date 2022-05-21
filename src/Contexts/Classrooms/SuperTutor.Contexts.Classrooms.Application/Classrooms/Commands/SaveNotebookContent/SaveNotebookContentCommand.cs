using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveNotebookContent;

public class SaveNotebookContentCommand : Command
{
    public SaveNotebookContentCommand(string classroomName, string notebookContent)
    {
        ClassroomName = classroomName;
        NotebookContent = notebookContent;
    }

    public string ClassroomName { get; }

    public string NotebookContent { get; }
}
