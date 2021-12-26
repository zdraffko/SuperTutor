using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments;

public class RedactionCommentId : GuidIdentifier
{
    public RedactionCommentId(Guid value) : base(value)
    {
    }
}
