using SuperTutor.Contexts.Classrooms.Domain.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms.Repositories;

internal class ClassroomRepository : IClassroomRepository
{
    private readonly IClassroomsDbContext classroomsDbContext;

    public ClassroomRepository(IClassroomsDbContext classroomsDbContext) => this.classroomsDbContext = classroomsDbContext;

    public void Add(Classroom classroom) => classroomsDbContext.Classrooms.Add(classroom);
}
