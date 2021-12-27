using SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public class Profile : Entity<ProfileId, Guid>, IAggregateRoot
{
    private readonly HashSet<TutoringGrade> tutoringGrades;
    private readonly List<RedactionComment> redactionComments;

    public Profile(
        UserId userId,
        string about,
        TutoringSubject tutoringSubject,
        HashSet<TutoringGrade> tutoringGrades,
        decimal rateForOneHour) : base(new ProfileId(Guid.NewGuid()))
    {
        UserId = userId;

        CheckInvariant(new ProfileAboutMustNotBeAboveTheMaxLenghtInvariant(about));
        About = about;

        TutoringSubject = tutoringSubject;

        CheckInvariant(new ProfileMustHaveAtLeastOneTutoringGradeInvariant(tutoringGrades));
        this.tutoringGrades = tutoringGrades;

        CheckInvariant(new ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(rateForOneHour));
        RateForOneHour = rateForOneHour;

        redactionComments = new List<RedactionComment>();
        Status = Status.ForReview;
        var currentDate = DateTime.UtcNow;
        CreationDate = currentDate;
        LastUpdateDate = currentDate;
    }

    public UserId UserId { get; }

    public string About { get; private set; }

    public TutoringSubject TutoringSubject { get; }

    public IReadOnlyCollection<TutoringGrade> TutoringGrades => tutoringGrades;

    public decimal RateForOneHour { get; private set; }

    public IReadOnlyCollection<RedactionComment> RedactionComments => redactionComments;

    public Status Status { get; private set; }

    public DateTime CreationDate { get; }

    public DateTime? LastApprovalDate { get; private set; }

    public AdminId? LastApprovalAdminId { get; private set; }

    public DateTime? LastModificationDate { get; private set; }

    public DateTime? LastRedactionRequestDate { get; private set; }

    public AdminId? LastRedactionRequestAdminId { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public void Approve(AdminId adminId)
    {
        CheckInvariant(new ProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant(Status));

        var unsettledRedactionComment = redactionComments.SingleOrDefault(redactionComment => redactionComment.IsSettled == false);
        if (unsettledRedactionComment != null)
        {
            unsettledRedactionComment.Settle(adminId);
        }

        var currentDate = DateTime.UtcNow;
        LastApprovalDate = currentDate;
        LastApprovalAdminId = adminId;
        Status = Status.Active;

        LastUpdateDate = currentDate;
    }

    public void RequestRedaction(RedactionComment redactionComment)
    {
        CheckInvariant(new ProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant(Status));

        var unsettledRedactionComment = redactionComments.SingleOrDefault(redactionComment => redactionComment.IsSettled == false);
        if (unsettledRedactionComment != null)
        {
            unsettledRedactionComment.Settle(redactionComment.CreatedByAdminId);
        }

        redactionComments.Add(redactionComment);
        CheckInvariant(new ProfileCanHaveOnlyOneActiveRedactionCommentInvariant(redactionComments));

        var currentDate = DateTime.UtcNow;
        LastRedactionRequestDate = currentDate;
        LastRedactionRequestAdminId = redactionComment.CreatedByAdminId;
        Status = Status.ForRedaction;

        LastUpdateDate = currentDate;
    }

    public void SubmitForReview()
    {
        CheckInvariant(new ProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));
        CheckInvariant(new ProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant(LastModificationDate, LastRedactionRequestDate));

        Status = Status.ForReview;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void Activate()
    {
        CheckInvariant(new ProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant(Status));
        CheckInvariant(new ProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant(LastModificationDate, LastApprovalDate));
        
        Status = Status.Active;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        CheckInvariant(new ProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant(Status));

        Status = Status.Inactive;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void UpdateAbout(string newAbout)
    {
        CheckInvariant(new ProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));
        CheckInvariant(new ProfileAboutMustNotBeAboveTheMaxLenghtInvariant(newAbout));

        var currentDate = DateTime.UtcNow;
        LastModificationDate = currentDate;
        About = newAbout;

        LastUpdateDate = currentDate;
    }

    public void AddTutoringGrades(HashSet<TutoringGrade> newTutoringGrades)
    {
        CheckInvariant(new ProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        if (newTutoringGrades.Count == 0)
        {
            return;
        }

        tutoringGrades.UnionWith(newTutoringGrades);

        LastUpdateDate = DateTime.UtcNow;
    }

    public void RemoveTutoringGrades(HashSet<TutoringGrade> tutoringGradesForRemoval)
    {
        CheckInvariant(new ProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        tutoringGrades.RemoveWhere(tutoringGrade => tutoringGradesForRemoval.Contains(tutoringGrade));
        CheckInvariant(new ProfileMustHaveAtLeastOneTutoringGradeInvariant(tutoringGrades));

        LastUpdateDate = DateTime.UtcNow;
    }

    public void IncreaseRateForOneHour(decimal increaseAmount)
    {
        CheckInvariant(new ProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        var newRateForOneHour = RateForOneHour + increaseAmount;
        CheckInvariant(new ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(newRateForOneHour));
        RateForOneHour = newRateForOneHour;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void DecreaseRateForOneHour(decimal decreaseAmount)
    {
        CheckInvariant(new ProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        var newRateForOneHour = RateForOneHour - decreaseAmount;
        CheckInvariant(new ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(newRateForOneHour));
        RateForOneHour = newRateForOneHour;

        LastUpdateDate = DateTime.UtcNow;
    }
}
