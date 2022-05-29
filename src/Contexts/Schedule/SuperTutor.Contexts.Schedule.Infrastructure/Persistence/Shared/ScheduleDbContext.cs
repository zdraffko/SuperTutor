using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Persistence.Shared;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options) { }

    public DbSet<TimeSlotQueryModel> TimeSlots { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("schedule");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
