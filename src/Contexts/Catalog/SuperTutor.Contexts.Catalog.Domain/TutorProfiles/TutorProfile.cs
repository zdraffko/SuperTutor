using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Constants;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.TutorProfiles;

public class TutorProfile : Entity<TutorProfileId, Guid>, IAggregateRoot
{
    private List<TutoringGrade> tutoringGrades;

    // Required for EntityFramework Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TutorProfile() : base(new TutorProfileId(Guid.Empty)) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public TutorProfile(TutorProfileId id, string about, TutoringSubject tutoringSubject, List<TutoringGrade> tutoringGrades, decimal rateForOneHour, bool isActive) : base(id)
    {
        About = about.Length > 100 ? about[..TutorProfileConstants.AboutMaxLength] : about;
        TutoringSubject = tutoringSubject;
        this.tutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
        IsActive = isActive;
    }

    public string About { get; private set; }

    public TutoringSubject TutoringSubject { get; }

    public IReadOnlyCollection<TutoringGrade> TutoringGrades => tutoringGrades;

    public decimal RateForOneHour { get; private set; }

    public bool IsActive { get; private set; }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void UpdateRateForOneHour(decimal newRateForOneHour) => RateForOneHour = newRateForOneHour;

    public void UpdateTutoringGrades(List<TutoringGrade> newTutoringGrades)
    {
        var finalTutoringGrades = new List<TutoringGrade>();

        foreach (var existingTutoringGrade in tutoringGrades)
        {
            if (newTutoringGrades.Contains(existingTutoringGrade))
            {
                finalTutoringGrades.Add(existingTutoringGrade);
            }
        }

        foreach (var newTutoringGrade in newTutoringGrades)
        {
            if (!tutoringGrades.Contains(newTutoringGrade))
            {
                finalTutoringGrades.Add(newTutoringGrade);
            }
        }

        tutoringGrades = finalTutoringGrades;
    }

    public void UpdateAbout(string newAbout) => About = newAbout;
}
