using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileMustHaveAtLeastOneTutoringGradeInvariant : Invariant
{
    private readonly HashSet<Grade> newTutoringGrades;

    public TutorProfileMustHaveAtLeastOneTutoringGradeInvariant(HashSet<Grade> newTutoringGrades)
        : base("The tutor profile must have at least one tutoring grade")
        => this.newTutoringGrades = newTutoringGrades;

    public override bool IsValid() => newTutoringGrades.Count > 0;
}
