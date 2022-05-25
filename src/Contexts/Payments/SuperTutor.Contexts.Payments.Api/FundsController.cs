using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Payments.Api;

public class FundsController : ApiController
{

    [HttpPost]
    public async Task<ActionResult> UpdateAccountPayoutInformation(UpdateAccountPayoutInformationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var options = new ExternalAccountCreateOptions
            {
                ExternalAccount = new AccountBankAccountOptions
                {
                    AccountHolderName = command.BankAccountHolderFullName,
                    AccountHolderType = command.BankAccountHolderType,
                    AccountNumber = command.BankAccountIban,
                    Country = "BG",
                    Currency = "BGN"
                },
                DefaultForCurrency = true
            };
            var service = new ExternalAccountService();
            await service.CreateAsync(command.ConnectedAccountId, options, cancellationToken: cancellationToken);

            return new OkResult();
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> UploadVerificationDocuments(
        [FromForm] string connectedAccountId,
        [FromForm] string connectedPersonId,
        IFormFile identityDocumentFront,
        IFormFile identityDocumentBack,
        IFormFile addressDocument,
        CancellationToken cancellationToken)
    {
        try
        {
            var fileService = new FileService();

            var identityDocumentFrontOptions = new FileCreateOptions
            {
                File = identityDocumentFront.OpenReadStream(),
                Purpose = FilePurpose.IdentityDocument
            };
            var identityDocumentFrontFile = await fileService.CreateAsync(identityDocumentFrontOptions, cancellationToken: cancellationToken);

            var identityDocumentBackOptions = new FileCreateOptions
            {
                File = identityDocumentBack.OpenReadStream(),
                Purpose = FilePurpose.IdentityDocument
            };
            var identityDocumentBackFile = await fileService.CreateAsync(identityDocumentBackOptions, cancellationToken: cancellationToken);

            var addressDocumentOptions = new FileCreateOptions
            {
                File = addressDocument.OpenReadStream(),
                Purpose = FilePurpose.IdentityDocument
            };
            var addressDocumentFile = await fileService.CreateAsync(addressDocumentOptions, cancellationToken: cancellationToken);

            var personService = new PersonService();

            var personUpdateOptions = new PersonUpdateOptions
            {
                Verification = new PersonVerificationOptions
                {
                    Document = new PersonVerificationDocumentOptions
                    {
                        Front = identityDocumentFrontFile.Id,
                        Back = identityDocumentBackFile.Id
                    },
                    AdditionalDocument = new PersonVerificationAdditionalDocumentOptions
                    {
                        Front = addressDocumentFile.Id
                    }
                }
            };

            var account = personService.UpdateAsync(connectedAccountId, connectedPersonId, personUpdateOptions, cancellationToken: cancellationToken);

            return new OkResult();
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }
    }

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

public class UpdateAccountPayoutInformationCommand
{
    public UpdateAccountPayoutInformationCommand(
        string connectedAccountId,
        string bankAccountHolderFullName,
        string bankAccountHolderType,
        string bankAccountIban)
    {
        ConnectedAccountId = connectedAccountId;
        BankAccountHolderFullName = bankAccountHolderFullName;
        BankAccountHolderType = bankAccountHolderType;
        BankAccountIban = bankAccountIban;
    }

    public string ConnectedAccountId { get; }

    public string BankAccountHolderFullName { get; }

    public string BankAccountHolderType { get; }

    public string BankAccountIban { get; }
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
