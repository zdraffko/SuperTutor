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

    public async Task<IEnumerable<StudentProfile>> GetAllForStudent(StudentId studentId, CancellationToken cancellationToken)
        => await profilesDbContext.StudentProfiles.Where(studentProfile => studentProfile.StudentId == studentId).ToListAsync(cancellationToken);

    public void Remove(StudentProfile studentProfile) => profilesDbContext.StudentProfiles.Remove(studentProfile);
}
