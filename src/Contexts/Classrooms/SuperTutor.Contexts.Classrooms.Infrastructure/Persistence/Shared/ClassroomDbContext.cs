using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Shared;

public class ClassroomDbContext : DbContext, IClassroomsDbContext
{
    public ClassroomDbContext(DbContextOptions<ClassroomDbContext> options) : base(options) { }

    public DbSet<Classroom> Classrooms { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("classrooms");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
