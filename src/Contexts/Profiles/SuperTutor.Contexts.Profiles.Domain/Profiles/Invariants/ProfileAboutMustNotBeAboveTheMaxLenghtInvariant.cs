using SuperTutor.Contexts.Profiles.Domain.Profiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileAboutMustNotBeAboveTheMaxLenghtInvariant : Invariant
{
    private readonly string newAbout;

    public ProfileAboutMustNotBeAboveTheMaxLenghtInvariant(string newAbout)
        : base($"About field is required and it cannot have more than {ProfileConstants.AboutMaxLenght} characters.")
    {
        this.newAbout = newAbout;
    }

    public override bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(newAbout))
        {
            return false;
        }

        if (newAbout.Length > ProfileConstants.AboutMaxLenght)
        {
            return false;
        }

        return true;
    }
}
