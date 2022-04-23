using MassTransit;
using SuperTutor.Contexts.Identity.IntegrationEvents.Users;
using SuperTutor.Contexts.Profiles.Application.Integration.Identity.Users.Commands.DeleteProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Infrastructure.IntegrationEvents.Consumers.Identity.Users;

public class UserDeletedIntegrationEventConsumer : IConsumer<UserDeletedIntegrationEvent>
{
    private readonly ICommandHandler<DeleteProfilesForUserCommand> deleteProfilesForUserCommandHandler;

    public UserDeletedIntegrationEventConsumer(ICommandHandler<DeleteProfilesForUserCommand> deleteProfilesForUserCommandHandler) => this.deleteProfilesForUserCommandHandler = deleteProfilesForUserCommandHandler;

    public async Task Consume(ConsumeContext<UserDeletedIntegrationEvent> context)
        => await deleteProfilesForUserCommandHandler.Handle(new DeleteProfilesForUserCommand(context.Message.UserId), context.CancellationToken);
}
