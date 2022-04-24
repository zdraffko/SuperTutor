using SuperTutor.Contexts.Catalog.Domain.Students;

namespace SuperTutor.Contexts.Catalog.Application.Students.Queries.Shared;

public interface IStudentsQueryRepository
{
    Task<IEnumerable<string>> GetAllFiltersForStudent(StudentId studentId, CancellationToken cancellationToken);
}
