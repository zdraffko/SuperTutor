namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

public abstract class Invariant
{
    public Invariant(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }

    public abstract bool IsValid();
}
