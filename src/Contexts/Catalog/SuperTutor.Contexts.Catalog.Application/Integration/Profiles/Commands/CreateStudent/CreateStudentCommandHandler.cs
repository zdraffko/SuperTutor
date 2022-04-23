using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.Commands.CreateStudent;

internal class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand>
{
    private readonly IStudentRepository studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository) => this.studentRepository = studentRepository;

    public Task<Result> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var student = new Student(command.StudentId);

        studentRepository.Add(student);

        return Task.FromResult(Result.Ok());
    }
}
