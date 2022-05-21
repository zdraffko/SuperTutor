using FluentResults;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Close;

internal class CloseClassroomCommandHandler : ICommandHandler<CloseClassroomCommand>
{
    private readonly IClassroomRepository classroomRepository;

    public CloseClassroomCommandHandler(IClassroomRepository classroomRepository) => this.classroomRepository = classroomRepository;

    public async Task<Result> Handle(CloseClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await classroomRepository.GetByName(command.ClassroomName, cancellationToken);
        if (classroom is null)
        {
            return Result.Fail($"Класна стая с име '{command.ClassroomName}' не съществува");
        }

        classroom.Close();

        return Result.Ok();
    }
}
