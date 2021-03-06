using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Shared.Persistence;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options) { }

    public DbSet<TimeSlotQueryModel> TimeSlots { get; set; } = default!;

    public DbSet<LessonQueryModel> Lessons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("schedule");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
