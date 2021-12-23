using SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public class Profile : Entity<ProfileId, int>, IAggregateRoot
{
    private HashSet<TutoringGrade> tutoringGrades;

    public Profile(
        UserId userId,
        string about,
        TutoringSubject tutoringSubject,
        HashSet<TutoringGrade> tutoringGrades,
        decimal rateForOneHour) : base(new ProfileId(0))
    {
        UserId = userId;

        CheckInvariant(new ProfileAboutMustNotBeAboveTheMaxLenghtInvariant(about));
        About = about;

        TutoringSubject = tutoringSubject;

        CheckInvariant(new ProfileMustHaveAtLeastOneTutoringGradeInvariant(tutoringGrades));
        this.tutoringGrades = tutoringGrades;

        CheckInvariant(new ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(rateForOneHour));
        RateForOneHour = rateForOneHour;

        Status = Status.ForReview;
        var currentDate = DateTime.UtcNow;
        CreationDate = currentDate;
        LastUpdateDate = currentDate;
    }

    public UserId UserId { get; private set; }

    public string About { get; private set; }

    public TutoringSubject TutoringSubject { get; private set; }

    public IReadOnlyCollection<TutoringGrade> TutoringGrades => tutoringGrades;

    public decimal RateForOneHour { get; private set; }

    public Status Status { get; private set; }

    public DateTime CreationDate { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public void Approve()
    {
        var newStatus = Status.Active;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void RequestRedaction()
    {
        var newStatus = Status.ForRedaction;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void SubmitForReview()
    {
        var newStatus = Status.ForReview;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void Activate()
    {
        var newStatus = Status.Active;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        var newStatus = Status.Inactive;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void UpdateAbout(string newAbout)
    {
        CheckInvariant(new ProfileAboutMustNotBeAboveTheMaxLenghtInvariant(newAbout));

        About = newAbout;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void AddTutoringGrades(HashSet<TutoringGrade> newTutoringGrades)
    {
        if (newTutoringGrades.Count == 0)
        {
            return;
        }

        tutoringGrades.UnionWith(newTutoringGrades);
        LastUpdateDate = DateTime.UtcNow;
    }

    public void RemoveTutoringGrades(HashSet<TutoringGrade> tutoringGradesForRemoval)
    {
        var tutoringGradesCopy = new HashSet<TutoringGrade>(tutoringGrades);
        tutoringGradesCopy.RemoveWhere(tutoringGrade => tutoringGradesForRemoval.Contains(tutoringGrade));

        CheckInvariant(new ProfileMustHaveAtLeastOneTutoringGradeInvariant(tutoringGradesCopy));

        tutoringGrades.RemoveWhere(tutoringGrade => tutoringGradesForRemoval.Contains(tutoringGrade));
        LastUpdateDate = DateTime.UtcNow;
    }

    public void IncreaseRateForOneHour(decimal increaseAmount)
    {
        var newRateForOneHour = RateForOneHour + increaseAmount;

        CheckInvariant(new ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(newRateForOneHour));

        RateForOneHour = newRateForOneHour;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void DecreaseRateForOneHour(decimal decreaseAmount)
    {
        var newRateForOneHour = RateForOneHour - decreaseAmount;

        CheckInvariant(new ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(newRateForOneHour));

        RateForOneHour = newRateForOneHour;
        LastUpdateDate = DateTime.UtcNow;
    }
}
