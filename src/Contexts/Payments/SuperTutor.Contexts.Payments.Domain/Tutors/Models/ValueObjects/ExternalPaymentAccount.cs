using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class ExternalPaymentAccount : ValueObject
{
    public ExternalPaymentAccount(string id, string personId)
    {
        Id = id;
        PersonId = personId;
    }

    public string Id { get; }

    public string PersonId { get; }
}
