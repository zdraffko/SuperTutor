using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateRateForOneHour;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileRateForOneHourDecreasedIntegrationEventConsumer : IConsumer<TutorProfileRateForOneHourDecreasedIntegrationEvent>
{
    private readonly ICommandHandler<UpdateRateForOneHourCommand> updateRateForOneHourCommandHandler;

    public TutorProfileRateForOneHourDecreasedIntegrationEventConsumer(ICommandHandler<UpdateRateForOneHourCommand> updateRateForOneHourCommandHandler) => this.updateRateForOneHourCommandHandler = updateRateForOneHourCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileRateForOneHourDecreasedIntegrationEvent> context)
        => await updateRateForOneHourCommandHandler.Handle(new UpdateRateForOneHourCommand(new TutorProfileId(context.Message.TutorProfileId), context.Message.NewRateForOneHour), context.CancellationToken);
}
