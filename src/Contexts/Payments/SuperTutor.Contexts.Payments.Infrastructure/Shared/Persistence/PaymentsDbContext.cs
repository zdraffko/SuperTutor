using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Payments.Application.Transfers.Queries;
using SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence.Models.TutorQuery;

namespace SuperTutor.Contexts.Payments.Infrastructure.Shared.Persistence;

public class PaymentsDbContext : DbContext
{
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options) { }

    public DbSet<TutorQueryModel> Tutors { get; set; } = default!;

    public DbSet<TransferQueryModel> Transfers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
