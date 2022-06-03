using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Domain.Charges;

public class ChargeId : Identifier<Guid>
{
    public ChargeId(Guid value) : base(value)
    {
    }
}
