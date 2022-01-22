using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;

public sealed class TutorProfileRedactionCommentStatus : Enumeration
{
    private TutorProfileRedactionCommentStatus(int value, string name) : base(value, name) { }

    public static readonly TutorProfileRedactionCommentStatus Active = new(0, nameof(Active));

    public static readonly TutorProfileRedactionCommentStatus SettledWithApprovement = new(1, nameof(SettledWithApprovement));

    public static readonly TutorProfileRedactionCommentStatus SettledWithNewRedactionRequest = new(2, nameof(SettledWithNewRedactionRequest));
}
