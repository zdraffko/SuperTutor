using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Classrooms.Domain.Classrooms;
public interface IClassroomRepository : IAggregateRootRepository<Classroom>
{
    void Add(Classroom classroom);

    Task<Classroom?> GetByName(string classroomName, CancellationToken cancellationToken);
}
