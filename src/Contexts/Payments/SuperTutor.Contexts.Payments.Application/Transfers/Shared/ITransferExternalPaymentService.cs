using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Transfers.Models.ValueObjects;

namespace SuperTutor.Contexts.Payments.Application.Transfers.Shared;

public interface ITransferExternalPaymentService
{
    Task<Result<ExternalPayment>> Create(ChargeId chargeId, string tutorExternalPaymentAccountId, decimal amount, string currency, CancellationToken cancellationToken);

}
