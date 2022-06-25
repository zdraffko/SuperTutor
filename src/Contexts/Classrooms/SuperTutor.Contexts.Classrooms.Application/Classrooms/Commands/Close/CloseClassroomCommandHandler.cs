using FluentResults;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Contracts;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Close;

internal class CloseClassroomCommandHandler : ICommandHandler<CloseClassroomCommand>
{
    private readonly IClassroomRepository classroomRepository;
    private readonly IClassroomHubService classroomHubService;

    public CloseClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomHubService classroomHubService)
    {
        this.classroomRepository = classroomRepository;
        this.classroomHubService = classroomHubService;
    }

    public async Task<Result> Handle(CloseClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await classroomRepository.GetByLessonId(command.LessonId, cancellationToken);
        if (classroom is null)
        {
            return Result.Fail($"Класна стая за урок с Id '{command.LessonId}' не съществува");
        }

        classroom.Close();

        await classroomHubService.CloseClassroom(classroom.Id);

        return Result.Ok();
    }
}
