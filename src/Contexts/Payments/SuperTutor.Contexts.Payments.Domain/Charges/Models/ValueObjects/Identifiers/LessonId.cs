using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Domain.Charges.Models.ValueObjects.Identifiers;

public class LessonId : Identifier<Guid>
{
    public LessonId(Guid value) : base(value)
    {
    }
}
