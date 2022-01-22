using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;

public class TutorProfileRedactionCommentId : GuidIdentifier
{
    public TutorProfileRedactionCommentId(Guid value) : base(value)
    {
    }
}
