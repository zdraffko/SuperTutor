using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileMustHaveAtLeastOneTutoringGradeInvariant : Invariant
{
    private readonly HashSet<TutoringGrade> newTutoringGrades;

    public ProfileMustHaveAtLeastOneTutoringGradeInvariant(HashSet<TutoringGrade> newTutoringGrades)
        : base("The profile must have at least one tutoring grade.")
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
