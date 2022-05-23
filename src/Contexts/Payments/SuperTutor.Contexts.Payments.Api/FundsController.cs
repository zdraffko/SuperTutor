using Microsoft.AspNetCore.Mvc;
using Stripe;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

namespace SuperTutor.Contexts.Payments.Api;

public class FundsController : ApiController
{
    [HttpPost]
    public async Task<ActionResult> CreateAccount(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var options = new AccountCreateOptions
            {
                Type = "custom",
                Country = "BG",
                Email = "testReg@testReg.com",
                Capabilities = new AccountCapabilitiesOptions
                {
                    Transfers = new AccountCapabilitiesTransfersOptions
                    {
                        Requested = true,
                    },
                },
                BusinessType = "individual",
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    Url = "https://superuchitel.bg"
                },
                Individual = new AccountIndividualOptions
                {
                    Email = "testReg@testReg.com"
                },
                DefaultCurrency = "BGN",
                Settings = new AccountSettingsOptions
                {
                    Payouts = new AccountSettingsPayoutsOptions
                    {
                        Schedule = new AccountSettingsPayoutsScheduleOptions
                        {
                            Interval = "manual"
                        }
                    }
                }
            };

            var service = new AccountService();

            var account = service.Create(options);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }

        return new OkResult();
    }

    [HttpPost]
    public async Task<ActionResult> UpdateAccountPersonalInformation(UpdateAccountPersonalInformationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            /*            var options = new PersonUpdateOptions
                        {
                            FirstName = command.FirstName,
                            LastName = command.LastName,
                            Dob = new DobOptions
                            {
                                Day = command.DateOfBirth.Day,
                                Month = command.DateOfBirth.Month,
                                Year = command.DateOfBirth.Year
                            }
                        };*/

            var options = new AccountUpdateOptions
            {
                Settings = new AccountSettingsOptions
                {
                    Payouts = new AccountSettingsPayoutsOptions
                    {
                        Schedule = new AccountSettingsPayoutsScheduleOptions
                        {
                            Interval = "manual"
                        }
                    },
                },
            };
            var service = new AccountService();

            var account = service.Update(command.ConnectedAccountId, options);
            /*            var service = new PersonService();

                        service.Update(command.ConnectedAccountId, command.ConnectedPersonId, options);*/
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }

        return new OkResult();
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
                    PostalCode = command.PostalCode
                }
            };

            var service = new PersonService();

            service.Update(command.ConnectedAccountId, command.ConnectedPersonId, options);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }

        return new OkResult();
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
            service.Create(command.ConnectedAccountId, options);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.Message);
        }

        return new OkResult();
    }
}

public class CreateAccountCommand
{

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
        string postalCode)
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

    public string PostalCode { get; }
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
