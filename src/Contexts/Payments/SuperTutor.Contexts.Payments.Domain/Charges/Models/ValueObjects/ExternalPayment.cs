using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects;

public class ExternalPayment : ValueObject
{
    public ExternalPayment(string id, string clientSecret)
    {
        Id = id;
        ClientSecret = clientSecret;
    }

    public string Id { get; }

    public string ClientSecret { get; }
}
