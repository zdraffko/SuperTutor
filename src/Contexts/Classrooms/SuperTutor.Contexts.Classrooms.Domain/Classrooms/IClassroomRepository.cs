using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Classrooms.Domain.Classrooms;
public interface IClassroomRepository : IAggregateRootRepository<Classroom>
{
    void Add(Classroom classroom);

    Task<Classroom?> GetById(ClassroomId classroomId, CancellationToken cancellationToken);

    Task<Classroom?> GetByLessonId(LessonId lessonId, CancellationToken cancellationToken);
}
