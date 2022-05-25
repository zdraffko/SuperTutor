using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Domain.Tutors;

public class TutorId : Identifier<Guid>
{
    public TutorId(Guid value) : base(value)
    {
    }
}
