using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForStudent;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForTutor;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.Shared;

public interface IClassroomQueryRepository
{
    Task<GetActiveClassroomForTutorQueryPayload> GetActiveForTutor(TutorId tutorId, CancellationToken cancellationToken);

    Task<GetActiveClassroomForStudentQueryPayload> GetActiveForStudent(StudentId studentId, CancellationToken cancellationToken);
}
