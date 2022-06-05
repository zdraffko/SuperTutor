using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Payments.Domain.Transfers.Models.ValueObjects;

public class ExternalPayment : ValueObject
{
    public ExternalPayment(string id) => Id = id;

    public string Id { get; }
}
