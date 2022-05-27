using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.Tutors;

public interface ITutorRepository : IAggregateRootRepository<Tutor>
{
    void Add(Tutor tutor);
}
