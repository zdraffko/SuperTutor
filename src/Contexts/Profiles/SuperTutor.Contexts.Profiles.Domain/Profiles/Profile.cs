using SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public class Profile : Entity<ProfileId, int>, IAggregateRoot
{
    private readonly HashSet<TutoringGrade> tutoringGrades;

    public Profile(
        UserId userId,
        string about,
        TutoringSubject tutoringSubject,
        HashSet<TutoringGrade> tutoringGrades,
        decimal rateForOneHour) : base(new ProfileId(0))
    {
        UserId = userId;
        About = about;
        TutoringSubject = tutoringSubject;
        this.tutoringGrades = tutoringGrades;
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
    }

    public void RequestRedaction()
    {
        var newStatus = Status.ForRedaction;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
    }

    public void SubmitForReview()
    {
        var newStatus = Status.ForReview;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
    }

    public void Activate()
    {
        var newStatus = Status.Active;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
    }

    public void Deactivate()
    {
        var newStatus = Status.Inactive;

        CheckInvariant(new ProfileStatusTransitionMustBeValidInvariant(Status, newStatus));

        Status = newStatus;
    }
}
