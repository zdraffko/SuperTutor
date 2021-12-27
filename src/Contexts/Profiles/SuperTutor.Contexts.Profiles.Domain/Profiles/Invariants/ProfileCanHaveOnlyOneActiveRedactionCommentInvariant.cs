using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanHaveOnlyOneActiveRedactionCommentInvariant : Invariant
{
    private readonly List<RedactionComment> redactionComments;

    public ProfileCanHaveOnlyOneActiveRedactionCommentInvariant(List<RedactionComment> redactionComments)
        : base("The profile can have only one active redaction comment.")
    {
        this.redactionComments = redactionComments;
    }

    public override bool IsValid()
    {
        var nonAddressedRedactionComments = redactionComments.Where(redactionComment => !redactionComment.IsSettled);
        if (nonAddressedRedactionComments.Count() > 1)
        {
            return false;
        }

        return true;
    }
}
