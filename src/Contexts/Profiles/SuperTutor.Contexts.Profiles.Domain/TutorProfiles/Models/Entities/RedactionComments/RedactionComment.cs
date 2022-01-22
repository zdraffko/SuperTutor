using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;

public class RedactionComment : Entity<RedactionCommentId, Guid>
{
    public RedactionComment(TutorProfileId tutorProfileId, AdminId createdByAdminId, string content) : base(new RedactionCommentId(Guid.NewGuid()))
    {
        TutorProfileId = tutorProfileId;
        CreatedByAdminId = createdByAdminId;

        CheckInvariant(new RedactionCommentContentMustNotBeAboveTheMaxLenghtInvariant(content));
        Content = content;

        Status = RedactionCommentStatus.Active;
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

    public RedactionCommentStatus Status { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public bool IsSettled => Status != RedactionCommentStatus.Active;

    public void SettleWithApprovement(AdminId settledByAdminId)
    {
        CheckInvariant(new RedactionCommentCannotBeSettledMoreThanOnceInvariant(Status));

        var currentDate = DateTime.UtcNow;

        SettledDate = currentDate;
        SettledByAdminId = settledByAdminId;
        Status = RedactionCommentStatus.SettledWithApprovement;

        LastUpdateDate = currentDate;
    }

    public void SettleWithNewRedactionRequest(AdminId settledByAdminId)
    {
        CheckInvariant(new RedactionCommentCannotBeSettledMoreThanOnceInvariant(Status));

        var currentDate = DateTime.UtcNow;

        SettledDate = currentDate;
        SettledByAdminId = settledByAdminId;
        Status = RedactionCommentStatus.SettledWithNewRedactionRequest;

        LastUpdateDate = currentDate;
    }
}
