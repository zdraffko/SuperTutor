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

        IsSettled = false;
        var currentDate = DateTime.UtcNow;
        CreationDate = currentDate;
        LastUpdateDate = currentDate;
    }

    public ProfileId ProfileId { get; }

    public AdminId CreatedByAdminId { get; }

    public DateTime CreationDate { get; }

    public string Content { get; private set; }

    public bool IsSettled { get; private set; }

    public DateTime? SettledDate { get; private set; }

    public AdminId? SettledByAdminId { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public void Settle(AdminId settledByAdminId)
    {
        IsSettled = true;
        SettledByAdminId = settledByAdminId;

        var currentDate = DateTime.UtcNow;
        SettledDate = currentDate;

        LastUpdateDate = currentDate;
    }
}
