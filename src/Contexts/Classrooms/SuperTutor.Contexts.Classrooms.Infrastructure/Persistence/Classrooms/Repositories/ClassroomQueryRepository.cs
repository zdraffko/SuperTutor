using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForStudent;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForTutor;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.Shared;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms.Repositories;

internal class ClassroomQueryRepository : IClassroomQueryRepository
{
    private readonly IClassroomsDbContext classroomsDbContext;

    public ClassroomQueryRepository(IClassroomsDbContext classroomsDbContext) => this.classroomsDbContext = classroomsDbContext;

    public async Task<GetActiveClassroomForTutorQueryPayload> GetActiveForTutor(TutorId tutorId, CancellationToken cancellationToken)
    {
        var classroomId = await classroomsDbContext.Classrooms
            .Where(classroom => classroom.TutorId == tutorId && classroom.IsActive)
            .Select(classroom => classroom.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return new GetActiveClassroomForTutorQueryPayload(classroomId);
    }

    public async Task<GetActiveClassroomForStudentQueryPayload> GetActiveForStudent(StudentId studentId, CancellationToken cancellationToken)
    {
        var classroomId = await classroomsDbContext.Classrooms
            .Where(classroom => classroom.StudentId == studentId && classroom.IsActive)
            .Select(classroom => classroom.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return new GetActiveClassroomForStudentQueryPayload(classroomId);
    }
}
