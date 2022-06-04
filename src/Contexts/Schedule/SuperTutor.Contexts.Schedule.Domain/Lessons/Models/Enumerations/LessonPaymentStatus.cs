using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

public class LessonPaymentStatus : Enumeration
{
    public LessonPaymentStatus(int value, string name) : base(value, name) { }

    public static readonly LessonPaymentStatus WaitingPayment = new(1, "Чака плащане");

    public static readonly LessonPaymentStatus Paid = new(2, "Платен");

    public static readonly LessonPaymentStatus Prepaid = new(3, "Предплатен");

    public static readonly LessonPaymentStatus NotPaidOnTime = new(4, "Не е платен навреме");

    public static readonly LessonPaymentStatus Refunded = new(5, "Възстановен");
}
