using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;

internal class TutorProfileRedactionCommentNewStatusMustHaveAValidTransitionFromTheOldStatusInvariant : Invariant
{
    private readonly TutorProfileRedactionCommentStatus currentStatus;
    private readonly TutorProfileRedactionCommentStatus newStatus;

    public TutorProfileRedactionCommentNewStatusMustHaveAValidTransitionFromTheOldStatusInvariant(TutorProfileRedactionCommentStatus currentStatus, TutorProfileRedactionCommentStatus newStatus)
        : base($"Cannot changed the tutor profile redaction comment status from '{currentStatus}' to '{newStatus}'")
    {
        this.currentStatus = currentStatus;
        this.newStatus = newStatus;
    }

    public override bool IsValid() => currentStatus.CanTransitionTo(newStatus);
}
