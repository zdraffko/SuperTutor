using FluentResults;
using Stripe;
using SuperTutor.Contexts.Payments.Application.Transfers.Shared;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Transfers.Models.ValueObjects;

namespace SuperTutor.Contexts.Payments.Infrastructure.Transfers.Shared;

internal class TransferExternalPaymentService : ITransferExternalPaymentService
{
    public async Task<Result<ExternalPayment>> Create(ChargeId chargeId, string tutorExternalPaymentAccountId, decimal amount, string currency, CancellationToken cancellationToken)
    {
        try
        {
            var transferOptions = new TransferCreateOptions
            {
                Amount = (long?) (amount * 100), // Amount should be in the smallest currency unit
                Currency = currency,
                Destination = tutorExternalPaymentAccountId,
                TransferGroup = chargeId.Value.ToString(),
            };

            var transferService = new TransferService();
            var transfer = await transferService.CreateAsync(transferOptions, cancellationToken: cancellationToken);

            var externalPayment = new ExternalPayment(transfer.Id);

            return Result.Ok(externalPayment);
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }
}
