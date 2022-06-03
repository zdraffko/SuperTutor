using FluentResults;
using SuperTutor.Contexts.Payments.Application.Charges.Shared;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Charges.Commands.Create;

internal class CreateChargeCommandHandler : ICommandHandler<CreateChargeCommand, CreateChargeCommandPayload>
{
    private readonly IChargeExternalPaymentService chargeExternalPaymentService;
    private readonly IAggregateRootEventsRepository<Charge, ChargeId, Guid> chargeRepository;

    public CreateChargeCommandHandler(IChargeExternalPaymentService chargeExternalPaymentService, IAggregateRootEventsRepository<Charge, ChargeId, Guid> chargeRepository)
    {
        this.chargeExternalPaymentService = chargeExternalPaymentService;
        this.chargeRepository = chargeRepository;
    }

    public async Task<Result<CreateChargeCommandPayload>> Handle(CreateChargeCommand command, CancellationToken cancellationToken)
    {
        var chargeId = new ChargeId(Guid.NewGuid());
        var currency = "bgn";

        var createExternalPaymentResult = await chargeExternalPaymentService.Create(chargeId, command.ChargeAmount, currency, cancellationToken);
        if (createExternalPaymentResult.IsFailed)
        {
            return createExternalPaymentResult.ToResult();
        }

        var charge = new Charge(chargeId, command.LessonId, command.StudentId, command.TutorId, command.ChargeAmount, currency, createExternalPaymentResult.Value);

        await chargeRepository.Add(charge, cancellationToken);

        var commandPayload = new CreateChargeCommandPayload(charge.ExternalPayment.ClientSecret);

        return Result.Ok(commandPayload);
    }
}
