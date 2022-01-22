using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;

internal class RedactionCommentCannotBeSettledMoreThanOnceInvariant : Invariant
{
    private readonly RedactionCommentStatus status;

    public RedactionCommentCannotBeSettledMoreThanOnceInvariant(RedactionCommentStatus status)
        : base($"The redaction comment is already settled with status '{status.Name}'")
    {
        this.status = status;
    }

    public override bool IsValid() => status == RedactionCommentStatus.Active;
}
