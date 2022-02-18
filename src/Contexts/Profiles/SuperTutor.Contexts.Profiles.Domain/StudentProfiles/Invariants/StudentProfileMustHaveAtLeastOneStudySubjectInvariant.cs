using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Invariants;

public class StudentProfileMustHaveAtLeastOneStudySubjectInvariant : Invariant
{
    private readonly HashSet<Subject> studySubjects;

    public StudentProfileMustHaveAtLeastOneStudySubjectInvariant(HashSet<Subject> studySubjects)
        : base("The student profile must have at least one study subject")
        => this.studySubjects = studySubjects;

    public override bool IsValid() => studySubjects.Count > 0;
}
