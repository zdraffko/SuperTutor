using MassTransit;
using SuperTutor.Contexts.Identity.IntegrationEvents.Users;
using SuperTutor.Contexts.Profiles.Application.Integration.Identity.Commands.DeleteProfiles;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Infrastructure.IntegrationEvents.Consumers.Identity;

public class UserDeletedIntegrationEventConsumer : IConsumer<UserDeletedIntegrationEvent>
{
    private readonly ICommandHandler<DeleteProfilesCommand> deleteProfilesCommandHandler;

    public UserDeletedIntegrationEventConsumer(ICommandHandler<DeleteProfilesCommand> deleteProfilesCommandHandler)
    {
        this.deleteProfilesCommandHandler = deleteProfilesCommandHandler;
    }

    public async Task Consume(ConsumeContext<UserDeletedIntegrationEvent> context)
        => await deleteProfilesCommandHandler.Handle(new DeleteProfilesCommand(new UserId(context.Message.UserId)), context.CancellationToken);
}
