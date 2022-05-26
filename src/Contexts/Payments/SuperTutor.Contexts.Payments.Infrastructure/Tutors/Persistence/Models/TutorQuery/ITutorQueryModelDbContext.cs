using Microsoft.EntityFrameworkCore;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence.Models.TutorQuery;

public interface ITutorQueryModelDbContext
{
    public DbSet<TutorQueryModel> Tutors { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
