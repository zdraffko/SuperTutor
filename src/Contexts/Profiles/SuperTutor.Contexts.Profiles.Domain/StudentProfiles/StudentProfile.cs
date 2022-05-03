using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;

namespace SuperTutor.Contexts.Profiles.Domain.StudentProfiles;

public class StudentProfile : Entity<StudentProfileId, Guid>, IAggregateRoot
{
    private readonly HashSet<Subject> studySubjects;

    public StudentProfile(
        StudentId studentId,
        HashSet<Subject> studySubjects,
        Grade studyGrade) : base(new StudentProfileId(Guid.NewGuid()))
    {
        StudentId = studentId;

        CheckInvariant(new StudentProfileMustHaveAtLeastOneStudySubjectInvariant(studySubjects));
        this.studySubjects = studySubjects;

        StudyGrade = studyGrade;

        var currentDate = DateTime.UtcNow;
        CreationDate = currentDate;
        LastUpdateDate = currentDate;
    }

    public StudentId StudentId { get; }

    public IReadOnlyCollection<Subject> StudySubjects => studySubjects;

    public Grade StudyGrade { get; private set; }

    public DateTime CreationDate { get; }

    public DateTime LastUpdateDate { get; private set; }

    public void AddStudySubjects(HashSet<Subject> newStudySubjects)
    {
        if (newStudySubjects.Count == 0)
        {
            return;
        }

        studySubjects.UnionWith(newStudySubjects);

        LastUpdateDate = DateTime.UtcNow;
    }

    public void RemoveStudySubjects(HashSet<Subject> studySubjectsForRemoval)
    {
        studySubjects.RemoveWhere(studySubject => studySubjectsForRemoval.Contains(studySubject));
        CheckInvariant(new StudentProfileMustHaveAtLeastOneStudySubjectInvariant(studySubjects));

        LastUpdateDate = DateTime.UtcNow;
    }

    public void UpdateStudyGrade(Grade newStudyGrade)
    {
        StudyGrade = newStudyGrade;

        LastUpdateDate = DateTime.UtcNow;
    }
}
