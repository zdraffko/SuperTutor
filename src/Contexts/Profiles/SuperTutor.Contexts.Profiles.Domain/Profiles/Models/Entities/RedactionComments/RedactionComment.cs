using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.Invariants;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments;

public class RedactionComment : Entity<RedactionCommentId, Guid>
{
    public RedactionComment(ProfileId profileId, AdminId createdByAdminId, string content) : base(new RedactionCommentId(Guid.NewGuid()))
    {
        ProfileId = profileId;  
        CreatedByAdminId = createdByAdminId;

        CheckInvariant(new RedactionCommentContentMustNotBeAboveTheMaxLenghtInvariant(content));
        Content = content;

        Status = Status.Active;
        var currentDate = DateTime.UtcNow;
        CreationDate = currentDate;
        LastUpdateDate = currentDate;
    }

    public ProfileId ProfileId { get; }

    public AdminId CreatedByAdminId { get; }

    public DateTime CreationDate { get; }

    public string Content { get; private set; }

    public DateTime? SettledDate { get; private set; }

    public AdminId? SettledByAdminId { get; private set; }

    public Status Status { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public bool IsSettled => Status != Status.Active;

    public void SettleWithApprovement(AdminId settledByAdminId)
    {
        CheckInvariant(new RedactionCommentCannotBeSettledMoreThanOnceInvariant(Status));

        var currentDate = DateTime.UtcNow;

        SettledDate = currentDate;
        SettledByAdminId = settledByAdminId;
        Status = Status.SettledWithApprovement;

        LastUpdateDate = currentDate;
    }

    public void SettleWithNewRedactionRequest(AdminId settledByAdminId)
    {
        CheckInvariant(new RedactionCommentCannotBeSettledMoreThanOnceInvariant(Status));

        var currentDate = DateTime.UtcNow;

        SettledDate = currentDate;
        SettledByAdminId = settledByAdminId;
        Status = Status.SettledWithNewRedactionRequest;

        LastUpdateDate = currentDate;
    }
}
