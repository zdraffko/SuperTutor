using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Students;

namespace SuperTutor.Contexts.Catalog.Persistence.Students;

public interface IStudentsDbContext
{
    public DbSet<Student> Students { get; }
}
