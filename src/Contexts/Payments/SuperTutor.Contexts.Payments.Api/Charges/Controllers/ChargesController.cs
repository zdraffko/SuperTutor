using Microsoft.AspNetCore.Mvc;
using Stripe;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Payments.Api.Charges.Controllers;

public class ChargesController : ApiController
{
    [HttpPost]
    public async Task<ActionResult<CreateChargeCommandPayload>> Create(CreateChargeCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long?) (command.ChargeAmount * 100), // Amount should be in the smallest currency unit,
                Currency = "bgn",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
                TransferGroup = command.LessonId.ToString()
            };

            var service = new PaymentIntentService();

            var paymentIntent = await service.CreateAsync(options, cancellationToken: cancellationToken);

            return Ok(new CreateChargeCommandPayload(paymentIntent.ClientSecret));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

public class CreateChargeCommand
{
    public CreateChargeCommand(decimal chargeAmount, Guid lessonId, Guid studentId, Guid tutorId)
    {
        ChargeAmount = chargeAmount;
        LessonId = lessonId;
        StudentId = studentId;
        TutorId = tutorId;
    }

    public decimal ChargeAmount { get; }

    public Guid LessonId { get; }

    public Guid StudentId { get; }

    public Guid TutorId { get; }
}

public class CreateChargeCommandPayload
{
    public CreateChargeCommandPayload(string paymentIntentSecret) => PaymentIntentSecret = paymentIntentSecret;

    public string PaymentIntentSecret { get; }
}
