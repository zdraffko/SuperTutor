using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.Students;

public interface IStudentRepository : IAggregateRootRepository<Student>
{
    void Add(Student student);

    Task<Student?> GetById(StudentId studentId, CancellationToken cancellationToken);
}
