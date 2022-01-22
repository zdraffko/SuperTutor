using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles;

public class TutorProfile : Entity<TutorProfileId, Guid>, IAggregateRoot
{
    private readonly HashSet<TutoringGrade> tutoringGrades;
    private readonly List<TutorProfileRedactionComment> redactionComments;

    public TutorProfile(
        UserId userId,
        string about,
        TutoringSubject tutoringSubject,
        HashSet<TutoringGrade> tutoringGrades,
        decimal rateForOneHour) : base(new TutorProfileId(Guid.NewGuid()))
    {
        UserId = userId;

        CheckInvariant(new TutorProfileAboutMustNotBeAboveTheMaxLenghtInvariant(about));
        About = about;

        TutoringSubject = tutoringSubject;

        CheckInvariant(new TutorProfileMustHaveAtLeastOneTutoringGradeInvariant(tutoringGrades));
        this.tutoringGrades = tutoringGrades;

        CheckInvariant(new TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(rateForOneHour));
        RateForOneHour = rateForOneHour;

        redactionComments = new List<TutorProfileRedactionComment>();
        Status = TutorProfileStatus.ForReview;
        var currentDate = DateTime.UtcNow;
        CreationDate = currentDate;
        LastUpdateDate = currentDate;
    }

    public UserId UserId { get; }

    public string About { get; private set; }

    public TutoringSubject TutoringSubject { get; }

    public IReadOnlyCollection<TutoringGrade> TutoringGrades => tutoringGrades;

    public decimal RateForOneHour { get; private set; }

    public IReadOnlyCollection<TutorProfileRedactionComment> RedactionComments => redactionComments;

    public TutorProfileStatus Status { get; private set; }

    public DateTime CreationDate { get; }

    public DateTime? LastApprovalDate { get; private set; }

    public AdminId? LastApprovalAdminId { get; private set; }

    public DateTime? LastModificationDate { get; private set; }

    public DateTime? LastRedactionRequestDate { get; private set; }

    public AdminId? LastRedactionRequestAdminId { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public void Approve(AdminId adminId)
    {
        CheckInvariant(new TutorProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant(Status));

        var unsettledRedactionComment = redactionComments.SingleOrDefault(redactionComment => !redactionComment.IsSettled);
        if (unsettledRedactionComment != null)
        {
            unsettledRedactionComment.SettleWithApprovement(adminId);
        }

        var currentDate = DateTime.UtcNow;
        LastApprovalDate = currentDate;
        LastApprovalAdminId = adminId;
        Status = TutorProfileStatus.Active;

        LastUpdateDate = currentDate;
    }

    public void RequestRedaction(TutorProfileRedactionComment redactionComment)
    {
        CheckInvariant(new TutorProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant(Status));

        var unsettledRedactionComment = redactionComments.SingleOrDefault(redactionComment => !redactionComment.IsSettled);
        if (unsettledRedactionComment != null)
        {
            unsettledRedactionComment.SettleWithNewRedactionRequest(redactionComment.CreatedByAdminId);
        }

        redactionComments.Add(redactionComment);
        CheckInvariant(new TutorProfileCanHaveOnlyOneActiveRedactionCommentInvariant(redactionComments));

        var currentDate = DateTime.UtcNow;
        LastRedactionRequestDate = currentDate;
        LastRedactionRequestAdminId = redactionComment.CreatedByAdminId;
        Status = TutorProfileStatus.ForRedaction;

        LastUpdateDate = currentDate;
    }

    public void SubmitForReview()
    {
        CheckInvariant(new TutorProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));
        CheckInvariant(new TutorProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant(LastModificationDate, LastRedactionRequestDate));

        Status = TutorProfileStatus.ForReview;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void Activate()
    {
        CheckInvariant(new TutorProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant(Status));
        CheckInvariant(new TutorProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant(LastModificationDate, LastApprovalDate));

        Status = TutorProfileStatus.Active;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        CheckInvariant(new TutorProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant(Status));

        Status = TutorProfileStatus.Inactive;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void UpdateAbout(string newAbout)
    {
        CheckInvariant(new TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));
        CheckInvariant(new TutorProfileAboutMustNotBeAboveTheMaxLenghtInvariant(newAbout));

        var currentDate = DateTime.UtcNow;
        LastModificationDate = currentDate;
        About = newAbout;

        LastUpdateDate = currentDate;
    }

    public void AddTutoringGrades(HashSet<TutoringGrade> newTutoringGrades)
    {
        CheckInvariant(new TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        if (newTutoringGrades.Count == 0)
        {
            return;
        }

        tutoringGrades.UnionWith(newTutoringGrades);

        LastUpdateDate = DateTime.UtcNow;
    }

    public void RemoveTutoringGrades(HashSet<TutoringGrade> tutoringGradesForRemoval)
    {
        CheckInvariant(new TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        tutoringGrades.RemoveWhere(tutoringGrade => tutoringGradesForRemoval.Contains(tutoringGrade));
        CheckInvariant(new TutorProfileMustHaveAtLeastOneTutoringGradeInvariant(tutoringGrades));

        LastUpdateDate = DateTime.UtcNow;
    }

    public void IncreaseRateForOneHour(decimal increaseAmount)
    {
        CheckInvariant(new TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        var newRateForOneHour = RateForOneHour + increaseAmount;
        CheckInvariant(new TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(newRateForOneHour));
        RateForOneHour = newRateForOneHour;

        LastUpdateDate = DateTime.UtcNow;
    }

    public void DecreaseRateForOneHour(decimal decreaseAmount)
    {
        CheckInvariant(new TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status));

        var newRateForOneHour = RateForOneHour - decreaseAmount;
        CheckInvariant(new TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(newRateForOneHour));
        RateForOneHour = newRateForOneHour;

        LastUpdateDate = DateTime.UtcNow;
    }
}
