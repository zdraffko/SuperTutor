using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;

public class PaymentId : Identifier<Guid>
{
    public PaymentId(Guid value) : base(value)
    {
    }
}
