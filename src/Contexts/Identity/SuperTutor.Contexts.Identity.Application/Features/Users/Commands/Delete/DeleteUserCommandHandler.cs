using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.IntegrationEvents.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Delete;

internal class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserService userService;
    private readonly IIntegrationEventsService integrationEventsService;

    public DeleteUserCommandHandler(IUserService userService, IIntegrationEventsService integrationEventsService)
    {
        this.userService = userService;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var deleteUserResult = await userService.Delete(command.Email);
        if (deleteUserResult.IsSuccess)
        {
            var deletedUserId = deleteUserResult.Value;
            integrationEventsService.Raise(new UserDeletedIntegrationEvent(deletedUserId));
        }

        return Result.Ok();
    }
}
