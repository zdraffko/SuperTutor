using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateAbout;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileAboutUpdatedIntegrationEventConsumer : IConsumer<TutorProfileAboutUpdatedIntegrationEvent>
{
    private readonly ICommandHandler<UpdateAboutForTutorProfileCommand> updateAboutForTutorProfileCommand;

    public TutorProfileAboutUpdatedIntegrationEventConsumer(ICommandHandler<UpdateAboutForTutorProfileCommand> updateAboutForTutorProfileCommand) => this.updateAboutForTutorProfileCommand = updateAboutForTutorProfileCommand;

    public async Task Consume(ConsumeContext<TutorProfileAboutUpdatedIntegrationEvent> context)
        => await updateAboutForTutorProfileCommand.Handle(new UpdateAboutForTutorProfileCommand(new TutorProfileId(context.Message.TutorProfileId), context.Message.NewAbout), context.CancellationToken);
}
