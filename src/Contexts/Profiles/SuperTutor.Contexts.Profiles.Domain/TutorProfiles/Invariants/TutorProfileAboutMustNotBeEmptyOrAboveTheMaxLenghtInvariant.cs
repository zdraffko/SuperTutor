using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant : Invariant
{
    private readonly string about;

    public TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant(string about)
        : base($"The 'about' field cannot be empty and it cannot have more than {TutorProfileConstants.AboutMaxLength} characters")
    {
        this.about = about;
    }

    public override bool IsValid() => !string.IsNullOrEmpty(about) && about.Length <= TutorProfileConstants.AboutMaxLength;
}
