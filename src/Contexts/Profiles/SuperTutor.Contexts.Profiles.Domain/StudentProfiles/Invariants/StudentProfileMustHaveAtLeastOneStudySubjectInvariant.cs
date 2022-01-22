using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Invariants;

internal class StudentProfileMustHaveAtLeastOneStudySubjectInvariant : Invariant
{
    private readonly HashSet<Subject> newStudySubject;

    public StudentProfileMustHaveAtLeastOneStudySubjectInvariant(HashSet<Subject> newStudySubject)
        : base("The student profile must have at least one study subject.")
    {
        this.newStudySubject = newStudySubject;
    }

    public override bool IsValid() => newStudySubject.Count > 0;
}
