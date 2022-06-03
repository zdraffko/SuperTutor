using FluentResults;
using Stripe;
using SuperTutor.Contexts.Payments.Application.Charges.Shared;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects;

namespace SuperTutor.Contexts.Payments.Infrastructure.Charges.Shared;

internal class ChargeExternalPaymentService : IChargeExternalPaymentService
{
    public async Task<Result<ExternalPayment>> Create(ChargeId chargeId, decimal amount, string currency, CancellationToken cancellationToken)
    {
        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long?) (amount * 100), // Amount should be in the smallest currency unit,
                Currency = currency,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
                TransferGroup = chargeId.ToString()
            };

            var service = new PaymentIntentService();

            var paymentIntent = await service.CreateAsync(options, cancellationToken: cancellationToken);

            var externalPayment = new ExternalPayment(paymentIntent.Id, paymentIntent.ClientSecret);

            return Result.Ok(externalPayment);
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }
}
