using FluentResults;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveWhiteboardContent;

internal class SaveWhiteboardContentCommandHandler : ICommandHandler<SaveWhiteboardContentCommand>
{
    private readonly IClassroomRepository classroomRepository;

    public SaveWhiteboardContentCommandHandler(IClassroomRepository classroomRepository) => this.classroomRepository = classroomRepository;

    public async Task<Result> Handle(SaveWhiteboardContentCommand command, CancellationToken cancellationToken)
    {
        var classroom = await classroomRepository.GetByName(command.ClassroomName, cancellationToken);
        if (classroom is null)
        {
            return Result.Fail($"Класна стая с име '{command.ClassroomName}' не съществува");
        }

        classroom.SaveWhiteboardContent(command.WhiteboardContent);

        return Result.Ok();
    }
}
