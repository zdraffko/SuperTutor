using SuperTutor.Contexts.Profiles.Domain.Profiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileAboutMustNotBeAboveTheMaxLenghtInvariant : Invariant
{
    private readonly string about;

    public ProfileAboutMustNotBeAboveTheMaxLenghtInvariant(string about)
        : base($"The 'about' field is required and it cannot have more than {ProfileConstants.AboutMaxLength} characters.")
    {
        this.about = about;
    }

    public override bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(about))
        {
            return false;
        }

        if (about.Length > ProfileConstants.AboutMaxLength)
        {
            return false;
        }

        return true;
    }
}
