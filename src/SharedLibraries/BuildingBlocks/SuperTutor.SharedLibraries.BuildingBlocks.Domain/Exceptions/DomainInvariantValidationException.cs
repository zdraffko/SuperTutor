using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Exceptions;

public class DomainInvariantValidationException : Exception
{
    public DomainInvariantValidationException(DomainInvariant brokenInvariant) : base(brokenInvariant.ErrorMessage)
    {
        BrokenInvariant = brokenInvariant;
    }

    public DomainInvariant BrokenInvariant { get; }

    public override string ToString() => $"{BrokenInvariant.GetType().FullName}: {BrokenInvariant.ErrorMessage}";
}
