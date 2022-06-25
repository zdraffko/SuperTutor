using SuperTutor.Contexts.Classrooms.Domain.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Contracts;

public interface IClassroomHubService
{
    Task CloseClassroom(ClassroomId classroomId);
}
