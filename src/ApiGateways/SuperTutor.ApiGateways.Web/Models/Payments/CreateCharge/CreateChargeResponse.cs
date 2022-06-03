using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.CreateCharge;

public class CreateChargeResponse
{
    [JsonPropertyName("paymentIntentSecret")]
    public string? PaymentIntentSecret { get; set; }
}
