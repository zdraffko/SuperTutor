using SuperTutor.Contexts.Payments.Domain.Shared.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Charges.Commands.Create;

public class CreateChargeCommand : Command<CreateChargeCommandPayload>
{
    public CreateChargeCommand(LessonId lessonId, StudentId studentId, TutorId tutorId, decimal chargeAmount)
    {
        LessonId = lessonId;
        StudentId = studentId;
        TutorId = tutorId;
        ChargeAmount = chargeAmount;
    }

    public LessonId LessonId { get; }

    public StudentId StudentId { get; }

    public TutorId TutorId { get; }

    public decimal ChargeAmount { get; }
}
