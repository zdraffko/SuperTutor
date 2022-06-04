using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.IntegrationEvents.Charges;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Charges.Commands.Complete;

internal class CompleteChargeCommandHandler : ICommandHandler<CompleteChargeCommand>
{
    private readonly IAggregateRootEventsRepository<Charge, ChargeId, Guid> chargeRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public CompleteChargeCommandHandler(IAggregateRootEventsRepository<Charge, ChargeId, Guid> chargeRepository, IIntegrationEventsService integrationEventsService)
    {
        this.chargeRepository = chargeRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(CompleteChargeCommand command, CancellationToken cancellationToken)
    {
        var charge = await chargeRepository.Load(command.ChargeId, cancellationToken);
        if (charge is null)
        {
            return Result.Fail($"Charge with Id {command.ChargeId} was not found");
        }

        charge.Complete();

        await chargeRepository.Update(charge, cancellationToken);

        integrationEventsService.Raise(new ChargeCompletedIntegrationEvent(
            chargeId: charge.Id.Value,
            lessonId: charge.LessonId.Value,
            tutorId: charge.TutorId.Value,
            studentId: charge.StudentId.Value));

        return Result.Ok();
    }
}
