using FluentResults;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Leave;

internal class LeaveClassroomCommandHandler : ICommandHandler<LeaveClassroomCommand>
{
    private readonly IClassroomRepository classroomRepository;

    public LeaveClassroomCommandHandler(IClassroomRepository classroomRepository) => this.classroomRepository = classroomRepository;

    public async Task<Result> Handle(LeaveClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await classroomRepository.GetByName(command.ClassroomName, cancellationToken);
        if (classroom is null)
        {
            return Result.Fail($"Класна стая с име '{command.ClassroomName}' не съществува");
        }

        classroom.Leave();

        return Result.Ok();
    }
}
