using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

internal class TutorProfileAboutMustNotBeAboveTheMaxLenghtInvariant : Invariant
{
    private readonly string about;

    public TutorProfileAboutMustNotBeAboveTheMaxLenghtInvariant(string about)
        : base($"The 'about' field is required and it cannot have more than {TutorProfileConstants.AboutMaxLength} characters.")
    {
        this.about = about;
    }

    public override bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(about))
        {
            return false;
        }

        if (about.Length > TutorProfileConstants.AboutMaxLength)
        {
            return false;
        }

        return true;
    }
}
