using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveNotebookContent;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.SaveWhiteboardContent;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Api.Hubs;

public class ConnectedUser
{
    public string Username { get; set; }

    public string UserId { get; set; }

    public string ConnectionId { get; set; }
}

[Authorize]
public class ClassroomHub : Hub
{
    private static readonly Dictionary<Guid, List<ConnectedUser>> Classrooms = new();
    private readonly ICommandHandler<SaveNotebookContentCommand> saveNotebookContentCommandHandler;
    private readonly ICommandHandler<SaveWhiteboardContentCommand> saveWhiteboardContentCommandHandler;

    public ClassroomHub(
        ICommandHandler<SaveNotebookContentCommand> saveNotebookContentCommandHandler,
        ICommandHandler<SaveWhiteboardContentCommand> saveWhiteboardContentCommandHandler)
    {
        this.saveNotebookContentCommandHandler = saveNotebookContentCommandHandler;
        this.saveWhiteboardContentCommandHandler = saveWhiteboardContentCommandHandler;
    }

    public async Task JoinClassroom(Guid classroomId, string username)
    {
        var connectedUser = new ConnectedUser
        {
            Username = username,
            UserId = Context.UserIdentifier,
            ConnectionId = Context.ConnectionId
        };

        if (Classrooms.TryGetValue(classroomId, out var classroom))
        {
            classroom.Add(connectedUser);
        }
        else
        {
            classroom = new List<ConnectedUser> { connectedUser };
            Classrooms.Add(classroomId, classroom);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, classroomId.ToString());

        await Clients.Caller.SendAsync("ClassroomJoined", classroom.Count > 1);
    }

    public async Task ConfirmClassroomJoin(string classroomId, string username) => await Clients.OthersInGroup(classroomId.ToString()).SendAsync("NewUserJoinedClassroom", username);

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        foreach (var classroom in Classrooms.Values)
        {
            var disconnectedUser = classroom.SingleOrDefault(user => user.ConnectionId == Context.ConnectionId);
            if (disconnectedUser is null)
            {
                continue;
            }

            classroom.Remove(disconnectedUser);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task Signal(Guid classroomId, string signalData) => await Clients.OthersInGroup(classroomId.ToString()).SendAsync("SignalReceived", signalData);

    public async Task LeaveClassroom(Guid classroomId)
    {
        if (Classrooms.TryGetValue(classroomId, out var classroom))
        {
            var disconnectedUser = classroom.SingleOrDefault(user => user.ConnectionId == Context.ConnectionId);

            if (disconnectedUser is null)
            {
                return;
            }

            classroom.Remove(disconnectedUser);

            await Clients.OthersInGroup(classroomId.ToString()).SendAsync("UserLeftClassroom", disconnectedUser.Username);

            await Clients.Caller.SendAsync("ClassroomLeft");
        }
    }

    public async Task SaveNotebookContent(Guid classroomId, string notebookContent)
    {
        await saveNotebookContentCommandHandler.Handle(new SaveNotebookContentCommand(new ClassroomId(classroomId), notebookContent), CancellationToken.None);

        await Clients.Caller.SendAsync("NotebookContentSaved", CancellationToken.None);
    }

    public async Task SaveWhiteboardContent(Guid classroomId, string whiteboardContent)
    {
        await saveWhiteboardContentCommandHandler.Handle(new SaveWhiteboardContentCommand(new ClassroomId(classroomId), whiteboardContent), CancellationToken.None);
        ;

        await Clients.Caller.SendAsync("WhiteboardContentSaved", CancellationToken.None);
    }
}
