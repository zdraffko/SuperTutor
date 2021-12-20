namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

public abstract class DomainInvariant
{
    public DomainInvariant(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }

    public abstract bool IsValid();
}
