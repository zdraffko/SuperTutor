namespace SuperTutor.Contexts.Payments.Infrastructure.Shared.Options;

public class ChargesWebhookEndpointsSecretsOptions
{
    public const string SectionName = "ChargesWebhookEndpointsSecrets";

    public string Succeeded { get; set; } = string.Empty;
}
