using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;

public class TutorProfileRedactionComment : Entity<TutorProfileRedactionCommentId, Guid>
{
    public TutorProfileRedactionComment(TutorProfileId tutorProfileId, AdminId createdByAdminId, string content) : base(new TutorProfileRedactionCommentId(Guid.NewGuid()))
    {
        TutorProfileId = tutorProfileId;
        CreatedByAdminId = createdByAdminId;

        CheckInvariant(new TutorProfileRedactionCommentContentMustNotBeEmptyOrAboveTheMaxLenghtInvariant(content));
        Content = content;

        Status = TutorProfileRedactionCommentStatus.Active;
        var currentDate = DateTime.UtcNow;
        CreationDate = currentDate;
        LastUpdateDate = currentDate;
    }

    public TutorProfileId TutorProfileId { get; }

    public AdminId CreatedByAdminId { get; }

    public DateTime CreationDate { get; }

    public string Content { get; private set; }

    public DateTime? SettledDate { get; private set; }

    public AdminId? SettledByAdminId { get; private set; }

    public TutorProfileRedactionCommentStatus Status { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public bool IsSettled => Status != TutorProfileRedactionCommentStatus.Active;

    internal void SettleWithApprovement(AdminId settledByAdminId)
    {
        CheckInvariant(new TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant(Status));

        TransitionStatus(TutorProfileRedactionCommentStatus.SettledWithApprovement);

        var currentDate = DateTime.UtcNow;
        SettledDate = currentDate;
        SettledByAdminId = settledByAdminId;

        LastUpdateDate = currentDate;
    }

    internal void SettleWithNewRedactionRequest(AdminId settledByAdminId)
    {
        CheckInvariant(new TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant(Status));

        TransitionStatus(TutorProfileRedactionCommentStatus.SettledWithNewRedactionRequest);

        var currentDate = DateTime.UtcNow;
        SettledDate = currentDate;
        SettledByAdminId = settledByAdminId;

        LastUpdateDate = currentDate;
    }

    private void TransitionStatus(TutorProfileRedactionCommentStatus newStatus)
    {
        CheckInvariant(new TutorProfileRedactionCommentNewStatusMustHaveAValidTransitionFromTheOldStatusInvariant(Status, newStatus));

        Status = newStatus;
    }
}
