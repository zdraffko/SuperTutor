using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.Enumerations;

public sealed class Status : Enumeration
{
    private Status(int value, string name) : base(value, name) { }

    public static readonly Status Active = new(0, nameof(Active));

    public static readonly Status SettledWithApprovement = new(1, nameof(SettledWithApprovement));

    public static readonly Status SettledWithNewRedactionRequest = new(2, nameof(SettledWithNewRedactionRequest));
}
