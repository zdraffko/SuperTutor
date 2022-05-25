using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects.Identifiers;

public class UserId : Identifier<Guid>
{
    public UserId(Guid value) : base(value)
    {
    }
}
