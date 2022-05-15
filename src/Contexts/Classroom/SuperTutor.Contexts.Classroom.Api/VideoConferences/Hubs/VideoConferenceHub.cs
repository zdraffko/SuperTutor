using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SuperTutor.Contexts.Classroom.Api.VideoConferences.Hubs;

[Authorize]
public class VideoConferenceHub : Hub
{
    private static readonly Dictionary<string, VideoConferenceRoom> VideoConferenceRooms = new();

    public async Task SendMessage(object message, string roomName)
    {
        await EmitLog("Client " + Context.ConnectionId + " said: " + message, roomName);

        await Clients.OthersInGroup(roomName).SendAsync("message", message);
    }

    public async Task CreateRoom(string roomName, string signalData)
    {
        await EmitLog("Received request to create " + roomName + " from client " + Context.ConnectionId, roomName);

        if (!VideoConferenceRooms.ContainsKey(roomName))
        {
            VideoConferenceRooms.Add(roomName, new VideoConferenceRoom(roomName, new VideoConferenceTutor("testTutor", signalData)));
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        await Clients.Caller.SendAsync("RoomCreated", new { from = "testTutor", signal = signalData });

        await EmitLog("Client " + Context.ConnectionId + " created the room " + roomName, roomName);

        await EmitLog("Room " + roomName + " now has " + 1 + " client(s)", roomName);
    }

    public async Task JoinRoom(string roomName, string signalData)
    {
        if (!VideoConferenceRooms.ContainsKey(roomName))
        {
            await EmitLog("Client " + Context.ConnectionId + " tried to join room " + roomName + " which does not exist", roomName);

            return;
        }

        var room = VideoConferenceRooms[roomName];
        room.Students.Add(new VideoConferenceStudent("testStudent"));

        await Clients.Group(roomName).SendAsync("StudentJoined", signalData);

    }

    public async Task JoinRoomInitial(string roomName)
    {
        await EmitLog("Client " + Context.ConnectionId + " tries to join room " + roomName, roomName);

        if (!VideoConferenceRooms.ContainsKey(roomName))
        {
            await EmitLog("Client " + Context.ConnectionId + " tried to join room " + roomName + " which does not exist", roomName);

            return;
        }


        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        var room = VideoConferenceRooms[roomName];
        room.Students.Add(new VideoConferenceStudent("testStudent"));

        await Clients.Caller.SendAsync("JoinedRoom", room.Tutor?.SignalData);

    }

    private async Task EmitLog(string message, string roomName) => await Clients.Group(roomName).SendAsync("log", "[Server]: " + message);
}

public class VideoConferenceRoom
{
    public VideoConferenceRoom(string name, VideoConferenceTutor? tutor)
    {
        Name = name;
        Tutor = tutor;
        Students = new List<VideoConferenceStudent>();
    }

    public string Name { get; }

    public VideoConferenceTutor? Tutor { get; }

    public List<VideoConferenceStudent> Students { get; }
}

public class VideoConferenceTutor
{
    public VideoConferenceTutor(string name, string signalData)
    {
        Name = name;
        SignalData = signalData;
    }

    public string Name { get; }

    public string SignalData { get; }
}

public class VideoConferenceStudent
{
    public VideoConferenceStudent(string name) => Name = name;

    public string Name { get; }
}
