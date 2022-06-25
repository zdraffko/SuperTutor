using Microsoft.AspNetCore.SignalR;
using SuperTutor.Contexts.Classrooms.Api.Hubs;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Contracts;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;

namespace SuperTutor.Contexts.Classrooms.Api.Services;

internal class ClassroomHubService : IClassroomHubService
{
    private readonly IHubContext<ClassroomHub> classroomHubContext;

    public ClassroomHubService(IHubContext<ClassroomHub> classroomHubContext) => this.classroomHubContext = classroomHubContext;

    public async Task CloseClassroom(ClassroomId classroomId)
        => await classroomHubContext.Clients.Group(classroomId.Value.ToString()).SendAsync("RoomClosed");
}
