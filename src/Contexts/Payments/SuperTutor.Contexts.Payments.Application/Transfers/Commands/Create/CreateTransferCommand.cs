using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Shared.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Transfers.Commands.Create;

public class CreateTransferCommand : Command
{
    public CreateTransferCommand(ChargeId chargeId, LessonId lessonId, StudentId studentId, TutorId tutorId)
    {
        ChargeId = chargeId;
        LessonId = lessonId;
        StudentId = studentId;
        TutorId = tutorId;
    }

    public ChargeId ChargeId { get; }

    public LessonId LessonId { get; }

    public StudentId StudentId { get; }

    public TutorId TutorId { get; }
}
