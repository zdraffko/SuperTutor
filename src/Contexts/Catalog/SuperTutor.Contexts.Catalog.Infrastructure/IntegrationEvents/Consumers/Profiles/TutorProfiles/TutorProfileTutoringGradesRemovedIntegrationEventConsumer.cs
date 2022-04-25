using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateTutoringGrades;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileTutoringGradesRemovedIntegrationEventConsumer : IConsumer<TutorProfileTutoringGradesRemovedIntegrationEvent>
{
    private readonly ICommandHandler<UpdateTutoringGradesCommand> updateTutoringGradesCommandHandler;

    public TutorProfileTutoringGradesRemovedIntegrationEventConsumer(ICommandHandler<UpdateTutoringGradesCommand> updateTutoringGradesCommandHandler) => this.updateTutoringGradesCommandHandler = updateTutoringGradesCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileTutoringGradesRemovedIntegrationEvent> context)
        => await updateTutoringGradesCommandHandler.Handle(new UpdateTutoringGradesCommand(
            new TutorProfileId(context.Message.TutorProfileId),
            context.Message.FinalTutoringGradesForTutorProfile.Select(tutoringGrade => new TutoringGrade(tutoringGrade.Value, tutoringGrade.Name))), context.CancellationToken);
}
