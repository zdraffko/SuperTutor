using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;

public abstract class Status : Enumeration
{
    private Status(int value, string name) : base(value, name) { }

    public static readonly Status Inactive = new InactiveStatus();

    public static readonly Status Active = new ActiveStatus();

    public static readonly Status ForReview = new ForReviewStatus();

    public static readonly Status ForRedaction = new ForRedactionStatus();

    public abstract bool CanTransitionTo(Status nextStatus);

    private sealed class InactiveStatus : Status
    {
        public InactiveStatus() : base(0, nameof(Inactive)) { }

        public override bool CanTransitionTo(Status newStatus) => newStatus == Active || newStatus == ForReview;
    }

    private sealed class ActiveStatus : Status
    {
        public ActiveStatus() : base(1, nameof(Active)) { }

        public override bool CanTransitionTo(Status newStatus) => newStatus == Inactive;
    }

    private sealed class ForReviewStatus : Status
    {
        public ForReviewStatus() : base(2, nameof(ForReview)) { }

        public override bool CanTransitionTo(Status newStatus) => newStatus == Active || newStatus == ForRedaction || newStatus == Inactive;
    }

    private sealed class ForRedactionStatus : Status
    {
        public ForRedactionStatus() : base(3, nameof(ForRedaction)) { }

        public override bool CanTransitionTo(Status newStatus) => newStatus == ForReview;
    }
}
