using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Payments.Domain.Charges.Models.Enumerations;

public class ChargeStatus : Enumeration
{
    private ChargeStatus(int value, string name) : base(value, name) { }

    public static readonly ChargeStatus Pending = new(1, nameof(Pending));

    public static readonly ChargeStatus Completed = new(2, nameof(Completed));

    public static readonly ChargeStatus Failed = new(3, nameof(Failed));

    public static readonly ChargeStatus Abandoned = new(4, nameof(Abandoned));
}
