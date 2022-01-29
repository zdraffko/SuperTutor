using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;

public class TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant : Invariant
{
    private readonly TutorProfileRedactionCommentStatus status;

    public TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant(TutorProfileRedactionCommentStatus status)
        : base($"The tutor profile redaction comment is already settled with status '{status.Name}'")
    {
        this.status = status;
    }

    public override bool IsValid() => status == TutorProfileRedactionCommentStatus.Active;
}
