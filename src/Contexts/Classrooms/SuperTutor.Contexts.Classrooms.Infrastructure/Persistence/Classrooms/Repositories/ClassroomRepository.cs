using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms.Repositories;

internal class ClassroomRepository : IClassroomRepository
{
    private readonly IClassroomsDbContext classroomsDbContext;

    public ClassroomRepository(IClassroomsDbContext classroomsDbContext) => this.classroomsDbContext = classroomsDbContext;

    public void Add(Classroom classroom) => classroomsDbContext.Classrooms.Add(classroom);

    public async Task<Classroom?> GetByName(string classroomName, CancellationToken cancellationToken)
        => await classroomsDbContext.Classrooms.SingleOrDefaultAsync(classroom => classroom.Name == classroomName, cancellationToken);
}
