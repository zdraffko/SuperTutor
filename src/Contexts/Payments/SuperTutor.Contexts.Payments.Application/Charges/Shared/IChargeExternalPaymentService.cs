using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects;

namespace SuperTutor.Contexts.Payments.Application.Charges.Shared;

public interface IChargeExternalPaymentService
{
    Task<Result<ExternalPayment>> Create(ChargeId chargeId, decimal amount, string currency, CancellationToken cancellationToken);
}
