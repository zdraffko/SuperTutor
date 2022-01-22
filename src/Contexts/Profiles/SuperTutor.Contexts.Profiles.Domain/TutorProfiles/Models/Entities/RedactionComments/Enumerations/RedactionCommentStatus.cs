using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;

public sealed class RedactionCommentStatus : Enumeration
{
    private RedactionCommentStatus(int value, string name) : base(value, name) { }

    public static readonly RedactionCommentStatus Active = new(0, nameof(Active));

    public static readonly RedactionCommentStatus SettledWithApprovement = new(1, nameof(SettledWithApprovement));

    public static readonly RedactionCommentStatus SettledWithNewRedactionRequest = new(2, nameof(SettledWithNewRedactionRequest));
}
