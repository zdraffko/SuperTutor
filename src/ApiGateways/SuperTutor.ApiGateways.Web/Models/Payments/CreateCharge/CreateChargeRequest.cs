namespace SuperTutor.ApiGateways.Web.Models.Payments.CreateCharge;

public class CreateChargeRequest
{
    public CreateChargeRequest(decimal chargeAmount, Guid lessonId, Guid tutorId)
    {
        ChargeAmount = chargeAmount;
        LessonId = lessonId;
        TutorId = tutorId;
    }

    public decimal ChargeAmount { get; }

    public Guid LessonId { get; }

    public Guid TutorId { get; }
}
