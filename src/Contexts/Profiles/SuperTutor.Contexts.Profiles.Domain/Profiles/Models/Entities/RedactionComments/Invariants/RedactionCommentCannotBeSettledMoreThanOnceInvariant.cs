using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.Invariants;

internal class RedactionCommentCannotBeSettledMoreThanOnceInvariant : Invariant
{
    private readonly Status status;

    public RedactionCommentCannotBeSettledMoreThanOnceInvariant(Status status)
        : base($"The redaction comment is already settled with status '{status.Name}'")
    {
        this.status = status;
    }

    public override bool IsValid() => status == Status.Active;
}
