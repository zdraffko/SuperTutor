using FluentResults;
using Microsoft.Extensions.Options;
using Stripe;
using SuperTutor.Contexts.Payments.Application.Charges.Commands.Complete;
using SuperTutor.Contexts.Payments.Application.Charges.Shared;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Infrastructure.Shared.Options;

namespace SuperTutor.Contexts.Payments.Infrastructure.Charges.Api;

internal class ChargesWebhookEventParser : IChargesWebhookEventParser
{
    private readonly string SucceededWebhookEndpointSecret;
    private readonly IOptionsSnapshot<ChargesWebhookEndpointsSecretsOptions> chargesWebhookEndpointsSecretsOptions;

    public ChargesWebhookEventParser(IOptionsSnapshot<ChargesWebhookEndpointsSecretsOptions> chargesWebhookEndpointsSecretsOptions) => SucceededWebhookEndpointSecret = chargesWebhookEndpointsSecretsOptions.Value.Succeeded;

    public Result<CompleteChargeCommand> ParseForCompleteChargeCommand(string rawEvent, string signature)
    {
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(rawEvent, signature, SucceededWebhookEndpointSecret);

            if (stripeEvent.Type != Events.PaymentIntentSucceeded)
            {
                return Result.Fail($"Expected event type {Events.PaymentIntentSucceeded} but received {stripeEvent.Type}");
            }

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            var rawChargeIdValue = paymentIntent.TransferGroup;
            if (!Guid.TryParse(rawChargeIdValue, out var chargeIdValue))
            {
                return Result.Fail($"Failed to parse transfer group value - {rawChargeIdValue}");
            }

            var completeChargeCommand = new CompleteChargeCommand(new ChargeId(chargeIdValue));

            return Result.Ok(completeChargeCommand);
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }
}
