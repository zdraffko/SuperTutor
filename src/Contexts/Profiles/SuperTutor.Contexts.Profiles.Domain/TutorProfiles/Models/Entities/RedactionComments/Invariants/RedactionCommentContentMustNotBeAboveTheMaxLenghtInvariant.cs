using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;

internal class RedactionCommentContentMustNotBeAboveTheMaxLenghtInvariant : Invariant
{
    private readonly string content;

    public RedactionCommentContentMustNotBeAboveTheMaxLenghtInvariant(string content)
        : base($"The 'content' field is required and it cannot have more than {RedactionCommentConstants.ContentMaxLength} characters.")
    {
        this.content = content;
    }

    public override bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return false;
        }

        if (content.Length > RedactionCommentConstants.ContentMaxLength)
        {
            return false;
        }

        return true;
    }
}
