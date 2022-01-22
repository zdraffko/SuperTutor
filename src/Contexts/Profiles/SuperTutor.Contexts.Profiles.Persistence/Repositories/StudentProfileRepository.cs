using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Persistence.Repositories;

public class StudentProfileRepository : IStudentProfileRepository
{
    private readonly ProfilesDbContext profilesDbContext;

    public StudentProfileRepository(ProfilesDbContext profilesDbContext)
    {
        this.profilesDbContext = profilesDbContext;
    }

    public void Add(StudentProfile studentProfile) => profilesDbContext.StudentProfiles.Add(studentProfile);

    public async Task<StudentProfile?> GetById(StudentProfileId studentProfileId, CancellationToken cancellationToken)
        => await profilesDbContext.StudentProfiles.SingleOrDefaultAsync(studentProfile => studentProfile.Id == studentProfileId, cancellationToken);

    public async Task<StudentProfile?> GetByStudentId(StudentId studentId, CancellationToken cancellationToken)
        => await profilesDbContext.StudentProfiles.SingleOrDefaultAsync(studentProfile => studentProfile.StudentId == studentId, cancellationToken);

    public void Remove(StudentProfile studentProfile) => profilesDbContext.StudentProfiles.Remove(studentProfile);
}
