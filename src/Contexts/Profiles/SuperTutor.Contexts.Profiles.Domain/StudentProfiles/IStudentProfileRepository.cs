using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.StudentProfiles;

public interface IStudentProfileRepository : IAggregateRootRepository<StudentProfile>
{
    void Add(StudentProfile studentProfile);

    Task<StudentProfile?> GetById(StudentProfileId studentProfileId, CancellationToken cancellationToken);

    Task<StudentProfile?> GetByStudentId(StudentId studentId, CancellationToken cancellationToken);

    void Remove(StudentProfile studentProfile);
}
