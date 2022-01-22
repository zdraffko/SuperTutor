using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.StudentProfiles;

public interface IStudentProfileRepository
{
    void Add(StudentProfile studentProfile);

    Task<StudentProfile?> GetById(StudentProfileId studentProfileId, CancellationToken cancellationToken);

    Task<IEnumerable<StudentProfile>> GetAllForStudent(StudentId studentId, CancellationToken cancellationToken);

    void Remove(StudentProfile studentProfile);
}
