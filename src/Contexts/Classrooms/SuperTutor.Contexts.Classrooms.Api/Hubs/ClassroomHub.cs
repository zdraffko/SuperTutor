using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Create;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Api.Hubs;

[Authorize]
public class ClassroomHub : Hub
{
    private static readonly Dictionary<string, VideoConferenceRoom> VideoConferenceRooms = new();

    private readonly ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler;

    public ClassroomHub(ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler) => this.createClassroomCommandHandler = createClassroomCommandHandler;

    public async Task CreateRoom(string classroomName, string tutorId)
    {
        if (!Guid.TryParse(tutorId, out var tutorIdValue))
        {
            await Clients.Caller.SendAsync("RoomCreationFailed", new { Message = "Невалидно учителско Id" });

            return;
        }

        await createClassroomCommandHandler.Handle(new CreateClassroomCommand(classroomName, new TutorId(tutorIdValue)), new CancellationTokenSource().Token);

        await Groups.AddToGroupAsync(Context.ConnectionId, classroomName);

        await Clients.Caller.SendAsync("RoomCreated", classroomName);
    }

    public async Task JoinRoom(string roomName, string studentName)
    {
        if (!VideoConferenceRooms.ContainsKey(roomName))
        {
            await Clients.Caller.SendAsync("JoinRoomFailed", new { Message = $"Стая с името '{roomName}' не съществува" });

            return;
        }

        var room = VideoConferenceRooms[roomName];
        var student = new VideoConferenceStudent(studentName, Context.ConnectionId);

        await Groups.AddToGroupAsync(student.ConnectionId, room.Name);
        room.Students.Add(student);

        //await Signal(room.Name, studentSignalData);

        //await Clients.OthersInGroup(roomName).SendAsync("StudentJoinedRoom", student.Name);
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
