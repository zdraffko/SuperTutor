using SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public class Profile : Entity<ProfileId, int>, IAggregateRoot
{
    private string about;

    private TutoringSubject tutoringSubject;

    private List<TutoringGrade> tutoringGrades;

    private decimal rateForOneHour;

    private Status status;

    public Profile(
        string about,
        TutoringSubject tutoringSubject,
        IEnumerable<TutoringGrade> tutoringGrades,
        decimal rateForOneHour) : base(new ProfileId(0))
    {
        this.tutoringSubject = tutoringSubject;
        this.about = about;
        this.tutoringGrades = tutoringGrades.ToList();
        this.rateForOneHour = rateForOneHour;
        status = Status.ForReview;
    }

    public void Approve()
    {
        var newStatus = Status.Active;

        CheckInvariant(new ProfileStatusTransitionMustBeValid(status, newStatus));

        status = newStatus;
    }

    public void RequestRedaction()
    {
        var newStatus = Status.ForRedaction;

        CheckInvariant(new ProfileStatusTransitionMustBeValid(status, newStatus));

        status = newStatus;
    }

    public void SubmitForReview()
    {
        var newStatus = Status.ForReview;

        CheckInvariant(new ProfileStatusTransitionMustBeValid(status, newStatus));

        status = newStatus;
    }

    public void Activate()
    {
        var newStatus = Status.Active;

        CheckInvariant(new ProfileStatusTransitionMustBeValid(status, newStatus));

        status = newStatus;
    }

    public void Deactivate()
    {
        var newStatus = Status.Inactive;

        CheckInvariant(new ProfileStatusTransitionMustBeValid(status, newStatus));

        status = newStatus;
    }
}
