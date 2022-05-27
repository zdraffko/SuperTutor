using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Tutors;

namespace SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Tutors;

public interface ITutorsDbContext
{
    public DbSet<Tutor> Tutors { get; }
}
