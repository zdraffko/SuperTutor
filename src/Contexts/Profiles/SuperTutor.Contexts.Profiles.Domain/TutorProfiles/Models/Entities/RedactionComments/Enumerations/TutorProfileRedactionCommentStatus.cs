using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;

public abstract class TutorProfileRedactionCommentStatus : Enumeration
{
    private TutorProfileRedactionCommentStatus(int value, string name) : base(value, name) { }

    public static readonly TutorProfileRedactionCommentStatus Active = new ActiveStatus();

    public static readonly TutorProfileRedactionCommentStatus SettledWithApprovement = new SettledWithApprovementStatus();

    public static readonly TutorProfileRedactionCommentStatus SettledWithNewRedactionRequest = new SettledWithNewRedactionRequestStatus();

    public abstract bool CanTransitionTo(TutorProfileRedactionCommentStatus newStatus);

    private sealed class ActiveStatus : TutorProfileRedactionCommentStatus
    {
        public ActiveStatus() : base(0, nameof(Active)) { }

        public override bool CanTransitionTo(TutorProfileRedactionCommentStatus newStatus) => newStatus == SettledWithApprovement || newStatus == SettledWithNewRedactionRequest;
    }

    private sealed class SettledWithApprovementStatus : TutorProfileRedactionCommentStatus
    {
        public SettledWithApprovementStatus() : base(1, nameof(SettledWithApprovement)) { }

        public override bool CanTransitionTo(TutorProfileRedactionCommentStatus newStatus) => false;
    }

    private sealed class SettledWithNewRedactionRequestStatus : TutorProfileRedactionCommentStatus
    {
        public SettledWithNewRedactionRequestStatus() : base(2, nameof(SettledWithNewRedactionRequest)) { }

        public override bool CanTransitionTo(TutorProfileRedactionCommentStatus newStatus) => false;
    }
}
