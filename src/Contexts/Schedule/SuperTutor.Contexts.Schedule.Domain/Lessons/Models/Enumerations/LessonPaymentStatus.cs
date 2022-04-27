using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

public class LessonPaymentStatus : Enumeration
{
    private LessonPaymentStatus(int value, string name) : base(value, name) { }

    public static readonly LessonPaymentStatus WaitingPayment = new(1, nameof(WaitingPayment));

    public static readonly LessonPaymentStatus Paid = new(2, nameof(Paid));

    public static readonly LessonPaymentStatus Prepaid = new(3, nameof(Prepaid));

    public static readonly LessonPaymentStatus NotPaidOnTime = new(4, nameof(NotPaidOnTime));

    public static readonly LessonPaymentStatus Refunded = new(5, nameof(Refunded));
}
