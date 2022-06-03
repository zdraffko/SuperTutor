namespace SuperTutor.Contexts.Payments.Application.Charges.Commands.Create;

public class CreateChargeCommandPayload
{
    public CreateChargeCommandPayload(string paymentIntentSecret) => PaymentIntentSecret = paymentIntentSecret;

    public string PaymentIntentSecret { get; }
}
