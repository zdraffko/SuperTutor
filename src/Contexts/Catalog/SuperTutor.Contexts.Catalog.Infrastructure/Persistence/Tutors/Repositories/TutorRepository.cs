using SuperTutor.Contexts.Catalog.Domain.Tutors;

namespace SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Tutors.Repositories;

internal class TutorRepository : ITutorRepository
{
    private readonly ITutorsDbContext tutorsDbContext;

    public TutorRepository(ITutorsDbContext tutorsDbContext) => this.tutorsDbContext = tutorsDbContext;

    public void Add(Tutor tutor) => tutorsDbContext.Tutors.Add(tutor);
}
