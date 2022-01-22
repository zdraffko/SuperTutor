using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

internal class TutorProfileMustHaveAtLeastOneTutoringGradeInvariant : Invariant
{
    private readonly HashSet<TutoringGrade> newTutoringGrades;

    public TutorProfileMustHaveAtLeastOneTutoringGradeInvariant(HashSet<TutoringGrade> newTutoringGrades)
        : base("The tutor profile must have at least one tutoring grade.")
    {
        this.newTutoringGrades = newTutoringGrades;
    }

    public override bool IsValid()
    {
        if (newTutoringGrades.Count == 0)
        {
            return false;
        }

        return true;
    }
}
