using FluentResults;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Create;

internal class CreateClassroomCommandHandler : ICommandHandler<CreateClassroomCommand>
{
    private readonly IClassroomRepository classroomRepository;

    public CreateClassroomCommandHandler(IClassroomRepository classroomRepository) => this.classroomRepository = classroomRepository;

    public async Task<Result> Handle(CreateClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = new Classroom(command.LessonId, command.TutorId, command.StudentId);

        classroomRepository.Add(classroom);

        return await Task.FromResult(Result.Ok());
    }
}
