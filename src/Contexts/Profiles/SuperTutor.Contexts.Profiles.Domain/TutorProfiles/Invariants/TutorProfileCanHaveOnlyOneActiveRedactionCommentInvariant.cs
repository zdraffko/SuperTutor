using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

internal class TutorProfileCanHaveOnlyOneActiveRedactionCommentInvariant : Invariant
{
    private readonly List<TutorProfileRedactionComment> redactionComments;

    public TutorProfileCanHaveOnlyOneActiveRedactionCommentInvariant(List<TutorProfileRedactionComment> redactionComments)
        : base("The tutor profile can have only one active redaction comment.")
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
