using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.IntegrationEvents.Payments.Charges.ChargeCompleted;

public class ScheduleLessonCommand : Command
{
    public ScheduleLessonCommand(LessonId lessonId, PaymentId paymentId)
    {
        LessonId = lessonId;
        PaymentId = paymentId;
    }

    public LessonId LessonId { get; }

    public PaymentId PaymentId { get; }
}
