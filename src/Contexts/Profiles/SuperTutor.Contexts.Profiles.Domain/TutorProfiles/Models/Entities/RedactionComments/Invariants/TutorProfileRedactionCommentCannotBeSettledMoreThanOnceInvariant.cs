using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;

public class TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant : Invariant
{
    private readonly TutorProfileRedactionCommentStatus currentStatus;

    public TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant(TutorProfileRedactionCommentStatus currentStatus)
        : base($"The tutor profile redaction comment is already settled with status '{currentStatus}'")
    {
        this.currentStatus = currentStatus;
    }

    public override bool IsValid() => currentStatus == TutorProfileRedactionCommentStatus.Active;
}
