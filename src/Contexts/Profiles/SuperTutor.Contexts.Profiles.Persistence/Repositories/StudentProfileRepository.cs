using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Persistence.Contexts.Contracts;

namespace SuperTutor.Contexts.Profiles.Persistence.Repositories;

public class StudentProfileRepository : IStudentProfileRepository
{
    private readonly IStudentProfilesDbContext studentProfilesDbContext;

    public StudentProfileRepository(IStudentProfilesDbContext studentProfilesDbContext) => this.studentProfilesDbContext = studentProfilesDbContext;

    public void Add(StudentProfile studentProfile) => studentProfilesDbContext.StudentProfiles.Add(studentProfile);

    public async Task<StudentProfile?> GetById(StudentProfileId studentProfileId, CancellationToken cancellationToken)
        => await studentProfilesDbContext.StudentProfiles.SingleOrDefaultAsync(studentProfile => studentProfile.Id == studentProfileId, cancellationToken);

    public async Task<StudentProfile?> GetByStudentId(StudentId studentId, CancellationToken cancellationToken)
        => await studentProfilesDbContext.StudentProfiles.SingleOrDefaultAsync(studentProfile => studentProfile.StudentId == studentId, cancellationToken);

    public void Remove(StudentProfile studentProfile) => studentProfilesDbContext.StudentProfiles.Remove(studentProfile);
}
