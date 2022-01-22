using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;

public class RedactionCommentId : GuidIdentifier
{
    public RedactionCommentId(Guid value) : base(value)
    {
    }
}
