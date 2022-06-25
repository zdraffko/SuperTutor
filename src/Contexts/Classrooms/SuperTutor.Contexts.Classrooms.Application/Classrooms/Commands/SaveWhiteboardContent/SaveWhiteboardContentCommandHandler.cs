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
        var classroom = await classroomRepository.GetById(command.ClassroomId, cancellationToken);
        if (classroom is null)
        {
            return Result.Fail($"Класна стая с Id '{command.ClassroomId}' не съществува");
        }

        classroom.SaveWhiteboardContent(command.WhiteboardContent);

        return Result.Ok();
    }
}
