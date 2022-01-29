using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;

public abstract class TutorProfileStatus : Enumeration
{
    private TutorProfileStatus(int value, string name) : base(value, name) { }

    public static readonly TutorProfileStatus Inactive = new InactiveStatus();

    public static readonly TutorProfileStatus Active = new ActiveStatus();

    public static readonly TutorProfileStatus ForReview = new ForReviewStatus();

    public static readonly TutorProfileStatus ForRedaction = new ForRedactionStatus();

    public abstract bool CanTransitionTo(TutorProfileStatus newStatus);

    private sealed class InactiveStatus : TutorProfileStatus
    {
        public InactiveStatus() : base(0, nameof(Inactive)) { }

        public override bool CanTransitionTo(TutorProfileStatus newStatus) => newStatus == Active || newStatus == ForReview;
    }

    private sealed class ActiveStatus : TutorProfileStatus
    {
        public ActiveStatus() : base(1, nameof(Active)) { }

        public override bool CanTransitionTo(TutorProfileStatus newStatus) => newStatus == Inactive;
    }

    private sealed class ForReviewStatus : TutorProfileStatus
    {
        public ForReviewStatus() : base(2, nameof(ForReview)) { }

        public override bool CanTransitionTo(TutorProfileStatus newStatus) => newStatus == Active || newStatus == ForRedaction || newStatus == Inactive;
    }

    private sealed class ForRedactionStatus : TutorProfileStatus
    {
        public ForRedactionStatus() : base(3, nameof(ForRedaction)) { }

        public override bool CanTransitionTo(TutorProfileStatus newStatus) => newStatus == ForReview;
    }
}
