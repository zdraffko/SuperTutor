using FluentResults;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Join;

internal class JoinClassroomCommandHandler : ICommandHandler<JoinClassroomCommand>
{
    private readonly IClassroomRepository classroomRepository;

    public JoinClassroomCommandHandler(IClassroomRepository classroomRepository) => this.classroomRepository = classroomRepository;

    public async Task<Result> Handle(JoinClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await classroomRepository.GetByName(command.ClassroomName, cancellationToken);
        if (classroom is null)
        {
            return Result.Fail($"Класна стая с име '{command.ClassroomName}' не съществува");
        }

        classroom.Join(command.StudentId);

        return Result.Ok();
    }
}
