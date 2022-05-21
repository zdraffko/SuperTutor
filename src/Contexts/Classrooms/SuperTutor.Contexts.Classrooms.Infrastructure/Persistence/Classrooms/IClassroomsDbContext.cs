using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms;

public interface IClassroomsDbContext
{
    public DbSet<Classroom> Classrooms { get; }
}
