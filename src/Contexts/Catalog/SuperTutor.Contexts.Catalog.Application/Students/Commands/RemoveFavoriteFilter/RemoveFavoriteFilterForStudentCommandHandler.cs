using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Students.Commands.RemoveFavoriteFilter;

internal class RemoveFavoriteFilterForStudentCommandHandler : ICommandHandler<RemoveFavoriteFilterForStudentCommand>
{
    private readonly IStudentRepository studentRepository;

    public RemoveFavoriteFilterForStudentCommandHandler(IStudentRepository studentRepository) => this.studentRepository = studentRepository;

    public async Task<Result> Handle(RemoveFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetById(command.StudentId, cancellationToken);
        if (student is null)
        {
            return Result.Fail("Student not found");
        }

        student.RemoveFavoriteFilter(command.Filter);

        return Result.Ok();
    }
}
