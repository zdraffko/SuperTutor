using Microsoft.AspNetCore.Mvc;
using Stripe;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Payments.Api;

public class FundsController : ApiController
{
    [HttpPost]
    public async Task<ActionResult> AcceptTermsOfService(AcceptTermsOfServiceCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var options = new AccountUpdateOptions
            {
                TosAcceptance = new AccountTosAcceptanceOptions
                {
                    ServiceAgreement = "full",
                    Date = DateTime.UtcNow,
                    Ip = command.UserIp
                }
            };

            var service = new AccountService();

            var account = await service.UpdateAsync(command.ConnectedAccountId, options, cancellationToken: cancellationToken);

            return new OkResult();
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }
    }
}

public class AcceptTermsOfServiceCommand
{
    public AcceptTermsOfServiceCommand(string connectedAccountId, string userIp)
    {
        ConnectedAccountId = connectedAccountId;
        UserIp = userIp;
    }

    public string ConnectedAccountId { get; }

    public string UserIp { get; }
}
