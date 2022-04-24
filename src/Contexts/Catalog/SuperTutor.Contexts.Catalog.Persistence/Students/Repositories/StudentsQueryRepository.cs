using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Application.Students.Queries.Shared;
using SuperTutor.Contexts.Catalog.Domain.Students;

namespace SuperTutor.Contexts.Catalog.Persistence.Students.Repositories;

internal class StudentsQueryRepository : IStudentsQueryRepository
{
    private readonly IStudentsDbContext studentsDbContext;

    public StudentsQueryRepository(IStudentsDbContext studentsDbContext) => this.studentsDbContext = studentsDbContext;

    public async Task<IEnumerable<string>> GetAllFiltersForStudent(StudentId studentId, CancellationToken cancellationToken)
        => await studentsDbContext.Students
            .AsNoTracking()
            .Where(student => student.Id == studentId)
            .SelectMany(student => student.FavoriteFilters.Select(favoriteFilter => favoriteFilter.Filter))
            .ToListAsync(cancellationToken);
}
