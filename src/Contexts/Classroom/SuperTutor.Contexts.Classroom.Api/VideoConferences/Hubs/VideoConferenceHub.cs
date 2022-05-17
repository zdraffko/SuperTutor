using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SuperTutor.Contexts.Classroom.Api.VideoConferences.Hubs;

[Authorize]
public class VideoConferenceHub : Hub
{
    private static readonly Dictionary<string, VideoConferenceRoom> VideoConferenceRooms = new();

    public async Task CreateRoom(string roomName, string tutorName)
    {
        if (VideoConferenceRooms.ContainsKey(roomName))
        {
            await Clients.Caller.SendAsync("RoomCreationFailed", new { Message = $"Стая с името '{roomName}' вече съществува" });

            return;
        }

        var tutor = new VideoConferenceTutor(tutorName, Context.ConnectionId);
        var room = new VideoConferenceRoom(roomName, tutor);

        VideoConferenceRooms.Add(room.Name, room);

        await Groups.AddToGroupAsync(tutor.ConnectionId, room.Name);

        await Clients.Caller.SendAsync("RoomCreated");
    }

    public async Task JoinRoom(string roomName, string studentName, string studentSignalData)
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

        await Clients.OthersInGroup(roomName).SendAsync("StudentJoinedRoom", new { StudentName = student.Name, StudentSignalData = studentSignalData });
    }

    public async Task AcknowledgeNewlyJoinedStudent(string roomName, string tutorSignalData) => await Clients.OthersInGroup(roomName).SendAsync("StudentAcknowledged", tutorSignalData);

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
