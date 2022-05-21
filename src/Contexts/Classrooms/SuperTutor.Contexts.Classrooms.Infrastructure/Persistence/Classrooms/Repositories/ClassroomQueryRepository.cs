using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.Shared;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms.Repositories;

internal class ClassroomQueryRepository : IClassroomQueryRepository
{
    private readonly IClassroomsDbContext classroomsDbContext;

    public ClassroomQueryRepository(IClassroomsDbContext classroomsDbContext) => this.classroomsDbContext = classroomsDbContext;

    public async Task<string?> GetStudentConnectionId(string classroomName)
        => await classroomsDbContext.Classrooms
            .AsNoTracking()
            .Where(classroom => classroom.Name == classroomName)
            .Select(classroom => classroom.StudentConnectionId)
            .SingleOrDefaultAsync();
}
