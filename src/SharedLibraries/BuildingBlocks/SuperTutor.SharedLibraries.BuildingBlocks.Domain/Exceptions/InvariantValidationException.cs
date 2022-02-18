using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Exceptions;

public class InvariantValidationException : Exception
{
    public InvariantValidationException(Invariant brokenInvariant) : base(brokenInvariant.ErrorMessage)
        => BrokenInvariant = brokenInvariant;

    public Invariant BrokenInvariant { get; }

    public override string ToString() => $"{BrokenInvariant.GetType().FullName}: {BrokenInvariant.ErrorMessage}";
}
