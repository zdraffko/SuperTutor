using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Payments.Api;

public class FundsController : ApiController
{
    [HttpPost]
    public async Task<ActionResult> UpdateAccountPersonalInformation(UpdateAccountPersonalInformationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var options = new PersonUpdateOptions
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Dob = new DobOptions
                {
                    Day = command.DateOfBirth.Day,
                    Month = command.DateOfBirth.Month,
                    Year = command.DateOfBirth.Year
                }
            };

            var service = new PersonService();

            await service.UpdateAsync(command.ConnectedAccountId, command.ConnectedPersonId, options, cancellationToken: cancellationToken);

            return new OkResult();
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> UpdateAccountAddressInformation(UpdateAccountAddressInformationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var options = new PersonUpdateOptions
            {
                Address = new AddressOptions
                {
                    State = command.State,
                    City = command.City,
                    Line1 = command.AddressLineOne,
                    Line2 = command.AddressLineTwo,
                    PostalCode = command.PostalCode.ToString()
                }
            };

            var service = new PersonService();

            await service.UpdateAsync(command.ConnectedAccountId, command.ConnectedPersonId, options, cancellationToken: cancellationToken);

            return new OkResult();
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }
    }

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

public class UpdateAccountPersonalInformationCommand
{
    public UpdateAccountPersonalInformationCommand(string connectedAccountId, string connectedPersonId, string firstName, string lastName, DateOnly dateOfBirth)
    {
        ConnectedAccountId = connectedAccountId;
        ConnectedPersonId = connectedPersonId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string ConnectedAccountId { get; }

    public string ConnectedPersonId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public DateOnly DateOfBirth { get; }
}

public class UpdateAccountAddressInformationCommand
{
    public UpdateAccountAddressInformationCommand(
        string connectedAccountId,
        string connectedPersonId,
        string state,
        string city,
        string addressLineOne,
        string addressLineTwo,
        int postalCode)
    {
        ConnectedAccountId = connectedAccountId;
        ConnectedPersonId = connectedPersonId;
        State = state;
        City = city;
        AddressLineOne = addressLineOne;
        AddressLineTwo = addressLineTwo;
        PostalCode = postalCode;
    }

    public string ConnectedAccountId { get; }

    public string ConnectedPersonId { get; }

    public string State { get; }

    public string City { get; }

    public string AddressLineOne { get; }

    public string AddressLineTwo { get; }

    public int PostalCode { get; }
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
