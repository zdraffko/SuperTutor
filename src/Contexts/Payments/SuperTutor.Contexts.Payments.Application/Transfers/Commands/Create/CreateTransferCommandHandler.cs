using FluentResults;
using SuperTutor.Contexts.Payments.Application.Transfers.Shared;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Transfers;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Transfers.Commands.Create;

internal class CreateTransferCommandHandler : ICommandHandler<CreateTransferCommand>
{
    private readonly IAggregateRootEventsRepository<Transfer, TransferId, Guid> transferRepository;
    private readonly IAggregateRootEventsRepository<Charge, ChargeId, Guid> chargeRepository;
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;
    private readonly ITransferExternalPaymentService transferExternalPaymentService;

    public CreateTransferCommandHandler(
        IAggregateRootEventsRepository<Transfer, TransferId, Guid> transferRepository,
        IAggregateRootEventsRepository<Charge, ChargeId, Guid> chargeRepository,
        IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository,
        ITransferExternalPaymentService transferExternalPaymentService)
    {
        this.transferRepository = transferRepository;
        this.chargeRepository = chargeRepository;
        this.tutorRepository = tutorRepository;
        this.transferExternalPaymentService = transferExternalPaymentService;
    }

    public async Task<Result> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
    {
        var charge = await chargeRepository.Load(command.ChargeId, cancellationToken);
        if (charge is null)
        {
            return Result.Fail($"Charge with Id {command.ChargeId} was not found");
        }

        var tutor = await tutorRepository.Load(command.TutorId, cancellationToken);
        if (tutor is null)
        {
            return Result.Fail($"Tutor with Id {command.TutorId} was not found");
        }

        var createExternalPaymentResult = await transferExternalPaymentService.Create(command.ChargeId, tutor.ExternalPaymentAccount.Id, charge.Amount, charge.Currency, cancellationToken);

        if (createExternalPaymentResult.IsFailed)
        {
            return createExternalPaymentResult.ToResult();
        }

        var transfer = new Transfer(charge.Id, command.LessonId, command.StudentId, tutor.Id, charge.Amount, charge.Currency, createExternalPaymentResult.Value);

        await transferRepository.Add(transfer, cancellationToken);

        return Result.Ok();
    }
}
