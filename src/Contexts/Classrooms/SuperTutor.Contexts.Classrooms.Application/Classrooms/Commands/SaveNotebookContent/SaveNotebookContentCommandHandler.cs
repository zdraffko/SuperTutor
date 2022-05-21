using FluentResults;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveNotebookContent;

internal class SaveNotebookContentCommandHandler : ICommandHandler<SaveNotebookContentCommand>
{
    private readonly IClassroomRepository classroomRepository;

    public SaveNotebookContentCommandHandler(IClassroomRepository classroomRepository) => this.classroomRepository = classroomRepository;

    public async Task<Result> Handle(SaveNotebookContentCommand command, CancellationToken cancellationToken)
    {
        var classroom = await classroomRepository.GetByName(command.ClassroomName, cancellationToken);
        if (classroom is null)
        {
            return Result.Fail($"Класна стая с име '{command.ClassroomName}' не съществува");
        }

        classroom.SaveNotebookContent(command.NotebookContent);

        return Result.Ok();
    }
}
