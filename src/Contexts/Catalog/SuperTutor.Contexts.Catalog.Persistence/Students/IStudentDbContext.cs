using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Students;

namespace SuperTutor.Contexts.Catalog.Persistence.FavoriteFilters;

public interface IStudentDbContext
{
    public DbSet<Student> Students { get; }
}
