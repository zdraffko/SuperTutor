using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Domain.Transfers;

public class TransferId : Identifier<Guid>
{
    public TransferId(Guid value) : base(value)
    {
    }
}
