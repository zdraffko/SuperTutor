using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Close;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Create;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Join;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Leave;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetStudentConnectionId;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Api.Hubs;

[Authorize]
public class ClassroomHub : Hub
{
    private readonly ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler;
    private readonly ICommandHandler<JoinClassroomCommand> joinClassroomCommandHandler;
    private readonly ICommandHandler<CloseClassroomCommand> closeClassroomCommandHandler;
    private readonly ICommandHandler<LeaveClassroomCommand> leaveClassroomCommandHandler;
    private readonly IQueryHandler<GetClassroomStudentConnectionIdQuery, GetClassroomStudentConnectionIdQueryPayload> getClassroomStudentConnectionIdQueryHandler;

    public ClassroomHub(
        ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler,
        ICommandHandler<JoinClassroomCommand> joinClassroomCommandHandler,
        ICommandHandler<CloseClassroomCommand> closeClassroomCommandHandler,
        ICommandHandler<LeaveClassroomCommand> leaveClassroomCommandHandler,
        IQueryHandler<GetClassroomStudentConnectionIdQuery, GetClassroomStudentConnectionIdQueryPayload> getClassroomStudentConnectionIdQueryHandler)
    {
        this.createClassroomCommandHandler = createClassroomCommandHandler;
        this.joinClassroomCommandHandler = joinClassroomCommandHandler;
        this.closeClassroomCommandHandler = closeClassroomCommandHandler;
        this.leaveClassroomCommandHandler = leaveClassroomCommandHandler;
        this.getClassroomStudentConnectionIdQueryHandler = getClassroomStudentConnectionIdQueryHandler;
    }

    public async Task CreateRoom(string classroomName, string tutorId)
    {
        var cancellationToken = new CancellationTokenSource().Token;

        if (!Guid.TryParse(tutorId, out var tutorIdValue))
        {
            await Clients.Caller.SendAsync("RoomCreationFailed", new { Message = "Невалидно учителско Id" }, cancellationToken: cancellationToken);

            return;
        }

        var createClassroomResult = await createClassroomCommandHandler.Handle(new CreateClassroomCommand(classroomName, new TutorId(tutorIdValue)), cancellationToken);
        if (createClassroomResult.IsFailed)
        {
            var errorMessage = createClassroomResult.Errors.FirstOrDefault()?.Message;
            await Clients.Caller.SendAsync("RoomCreationFailed", new { Message = errorMessage }, cancellationToken: cancellationToken);

            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, classroomName, cancellationToken);

        await Clients.Caller.SendAsync("RoomCreated", classroomName, cancellationToken);
    }

    public async Task JoinRoom(string classroomName, string studentId)
    {
        var cancellationToken = new CancellationTokenSource().Token;

        if (!Guid.TryParse(studentId, out var studentIdValue))
        {
            await Clients.Caller.SendAsync("JoinRoomFailed", new { Message = "Невалидно учителско Id" }, cancellationToken: cancellationToken);

            return;
        }

        var joinClassroomResult = await joinClassroomCommandHandler.Handle(new JoinClassroomCommand(classroomName, new StudentId(studentIdValue), Context.ConnectionId), cancellationToken);
        if (joinClassroomResult.IsFailed)
        {
            var errorMessage = joinClassroomResult.Errors.FirstOrDefault()?.Message;
            await Clients.Caller.SendAsync("JoinRoomFailed", new { Message = errorMessage }, cancellationToken: cancellationToken);

            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, classroomName, cancellationToken);
    }

    public async Task ConfirmJoinRoom(string roomName, string studentName) => await Clients.OthersInGroup(roomName).SendAsync("StudentJoinedRoom", studentName);

    public async Task Signal(string roomName, string signalData) => await Clients.OthersInGroup(roomName).SendAsync("SignalReceived", signalData);

    public async Task CloseRoom(string classroomName)
    {
        var cancellationToken = new CancellationTokenSource().Token;

        await closeClassroomCommandHandler.Handle(new CloseClassroomCommand(classroomName), cancellationToken);

        await Clients.Group(classroomName).SendAsync("RoomClosed", classroomName, cancellationToken: cancellationToken);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, classroomName, cancellationToken);

        var queryResult = await getClassroomStudentConnectionIdQueryHandler.Handle(new GetClassroomStudentConnectionIdQuery(classroomName), cancellationToken);
        await Groups.RemoveFromGroupAsync(queryResult.Value.StudentConnectionId, classroomName, cancellationToken);
    }

    public async Task LeaveRoom(string classroomName, string studentName)
    {
        var cancellationToken = new CancellationTokenSource().Token;

        var queryResult = await getClassroomStudentConnectionIdQueryHandler.Handle(new GetClassroomStudentConnectionIdQuery(classroomName), cancellationToken);

        var leaveClassroomResult = await leaveClassroomCommandHandler.Handle(new LeaveClassroomCommand(classroomName), cancellationToken);
        if (leaveClassroomResult.IsFailed)
        {
            // TODO Handle this case
            return;
        }

        await Clients.Caller.SendAsync("RoomLeft", classroomName);
        await Clients.OthersInGroup(classroomName).SendAsync("StudentLeftRoom", studentName);
        await Groups.RemoveFromGroupAsync(queryResult.Value.StudentConnectionId, classroomName);
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
