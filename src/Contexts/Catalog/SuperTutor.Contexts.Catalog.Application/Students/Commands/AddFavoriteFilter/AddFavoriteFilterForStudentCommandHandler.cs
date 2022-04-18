using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Students.Commands.AddFavoriteFilter;

internal class AddFavoriteFilterForStudentCommandHandler : ICommandHandler<AddFavoriteFilterForStudentCommand>
{
    private readonly IStudentRepository studentRepository;

    public AddFavoriteFilterForStudentCommandHandler(IStudentRepository studentRepository) => this.studentRepository = studentRepository;

    public async Task<Result> Handle(AddFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetById(command.StudentId, cancellationToken);
        if (student is null)
        {
            return Result.Fail("Student not found");
        }

        var favoriteFilter = new FavoriteFilter(command.StudentId, command.Filter);

        student.AddFavoriteFilter(favoriteFilter);

        return Result.Ok();
    }
}
