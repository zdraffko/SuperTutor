using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Create;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Join;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Api.Hubs;

[Authorize]
public class ClassroomHub : Hub
{
    private static readonly Dictionary<string, VideoConferenceRoom> VideoConferenceRooms = new();

    private readonly ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler;
    private readonly ICommandHandler<JoinClassroomCommand> joinClassroomCommandHandler;

    public ClassroomHub(ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler, ICommandHandler<JoinClassroomCommand> joinClassroomCommandHandler)
    {
        this.createClassroomCommandHandler = createClassroomCommandHandler;
        this.joinClassroomCommandHandler = joinClassroomCommandHandler;
    }

    public async Task CreateRoom(string classroomName, string tutorId)
    {
        if (!Guid.TryParse(tutorId, out var tutorIdValue))
        {
            await Clients.Caller.SendAsync("RoomCreationFailed", new { Message = "Невалидно учителско Id" });

            return;
        }

        var createClassroomResult = await createClassroomCommandHandler.Handle(new CreateClassroomCommand(classroomName, new TutorId(tutorIdValue)), new CancellationTokenSource().Token);
        if (createClassroomResult.IsFailed)
        {
            var errorMessage = createClassroomResult.Errors.FirstOrDefault()?.Message;
            await Clients.Caller.SendAsync("RoomCreationFailed", new { Message = errorMessage });

            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, classroomName);

        await Clients.Caller.SendAsync("RoomCreated", classroomName);
    }

    public async Task JoinRoom(string classroomName, string studentId)
    {
        if (!Guid.TryParse(studentId, out var studentIdValue))
        {
            await Clients.Caller.SendAsync("JoinRoomFailed", new { Message = "Невалидно учителско Id" });

            return;
        }

        var joinClassroomResult = await joinClassroomCommandHandler.Handle(new JoinClassroomCommand(classroomName, new StudentId(studentIdValue)), new CancellationTokenSource().Token);
        if (joinClassroomResult.IsFailed)
        {
            var errorMessage = joinClassroomResult.Errors.FirstOrDefault()?.Message;
            await Clients.Caller.SendAsync("JoinRoomFailed", new { Message = errorMessage });

            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, classroomName);
    }

    public async Task ConfirmJoinRoom(string roomName, string studentName) => await Clients.OthersInGroup(roomName).SendAsync("StudentJoinedRoom", studentName);

    public async Task Signal(string roomName, string signalData) => await Clients.OthersInGroup(roomName).SendAsync("SignalReceived", signalData);

    public async Task CloseRoom(string roomName)
    {
        var roomForRemoval = VideoConferenceRooms[roomName];

        await Clients.Group(roomName).SendAsync("RoomClosed", roomForRemoval.Name);

        await Groups.RemoveFromGroupAsync(roomForRemoval.Tutor.ConnectionId, roomForRemoval.Name);
        foreach (var student in roomForRemoval.Students)
        {
            await Groups.RemoveFromGroupAsync(student.ConnectionId, roomForRemoval.Name);
        }

        VideoConferenceRooms.Remove(roomName);
    }

    public async Task LeaveRoom(string roomName, string studentName)
    {
        var room = VideoConferenceRooms[roomName];
        var studentForRemoval = room.Students.FirstOrDefault(student => student.Name == studentName);

        if (studentForRemoval is null)
        {
            return;
        }

        await Clients.Caller.SendAsync("RoomLeft", room.Name);
        await Clients.OthersInGroup(room.Name).SendAsync("StudentLeftRoom", studentForRemoval.Name);

        await Groups.RemoveFromGroupAsync(studentForRemoval.ConnectionId, room.Name);

        room.Students.Remove(studentForRemoval);
    }

    public async Task SaveNotebookContent(string notebookContent)
    {
        await Task.Delay(2000);
        await Clients.Caller.SendAsync("NotebookContentSaved");
    }

    public async Task SaveWhiteboardContent(string whiteboardContent)
    {
        await Task.Delay(2000);
        await Clients.Caller.SendAsync("WhiteboardContentSaved");
    }
}

public class VideoConferenceRoom
{
    public VideoConferenceRoom(string name, VideoConferenceTutor tutor)
    {
        Name = name;
        Tutor = tutor;
        Students = new List<VideoConferenceStudent>();
    }

    public string Name { get; }

    public VideoConferenceTutor Tutor { get; }

    public List<VideoConferenceStudent> Students { get; }
}

public class VideoConferenceTutor
{
    public VideoConferenceTutor(string name, string connectionId)
    {
        Name = name;
        ConnectionId = connectionId;
    }

    public string Name { get; }

    public string ConnectionId { get; }
}

public class VideoConferenceStudent
{
    public VideoConferenceStudent(string name, string connectionId)
    {
        Name = name;
        ConnectionId = connectionId;
    }

    public string Name { get; }

    public string ConnectionId { get; }
}
