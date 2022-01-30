using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;

namespace SuperTutor.Contexts.Profiles.Persistence.Contexts.Contracts;

public interface IStudentProfilesDbContext
{
    public DbSet<StudentProfile> StudentProfiles { get; set; }
}
