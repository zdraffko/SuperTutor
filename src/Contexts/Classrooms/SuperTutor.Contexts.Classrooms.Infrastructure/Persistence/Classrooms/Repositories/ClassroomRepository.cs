using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms.Repositories;

internal class ClassroomRepository : IClassroomRepository
{
    private readonly IClassroomsDbContext classroomsDbContext;

    public ClassroomRepository(IClassroomsDbContext classroomsDbContext) => this.classroomsDbContext = classroomsDbContext;

    public void Add(Classroom classroom) => classroomsDbContext.Classrooms.Add(classroom);

    public async Task<Classroom?> GetById(ClassroomId classroomId, CancellationToken cancellationToken)
        => await classroomsDbContext.Classrooms.SingleOrDefaultAsync(classroom => classroom.Id == classroomId, cancellationToken);

    public async Task<Classroom?> GetByLessonId(LessonId lessonId, CancellationToken cancellationToken)
        => await classroomsDbContext.Classrooms.SingleOrDefaultAsync(classroom => classroom.LessonId == lessonId, cancellationToken);
}
