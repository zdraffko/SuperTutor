using FluentResults;
using Stripe;
using SuperTutor.Contexts.Payments.Application.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Shared;

internal class TutorExternalPaymentService : ITutorExternalPaymentService
{
    public async Task<Result<(string accountId, string personId)>> CreateAccount(TutorId tutorId, string tutorEmail, CancellationToken cancellationToken)
    {
        try
        {
            var accountCreateOptions = new AccountCreateOptions
            {
                Type = "custom",
                Country = "BG",
                Email = tutorEmail,
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
                    Email = tutorEmail
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
                },
                Metadata = new Dictionary<string, string>
                {
                    { "UserId", tutorId.Value.ToString() }
                }
            };

            var accountService = new AccountService();

            var account = await accountService.CreateAsync(accountCreateOptions, cancellationToken: cancellationToken);

            return Result.Ok((account.Id, account.Individual.Id));
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }

    public async Task<Result> UpdatePersonalInformation(string accountId, string personId, PersonalInformation personalInformation, CancellationToken cancellationToken)
    {
        try
        {
            var personUpdateOptions = new PersonUpdateOptions
            {
                FirstName = personalInformation.FirstName,
                LastName = personalInformation.LastName,
                Dob = new DobOptions
                {
                    Day = personalInformation.DateOfBirthDay,
                    Month = personalInformation.DateOfBirthMonth,
                    Year = personalInformation.DateOfBirthYear
                }
            };

            var personService = new PersonService();

            await personService.UpdateAsync(accountId, personId, personUpdateOptions, cancellationToken: cancellationToken);

            return Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }

    public async Task<Result> UpdateAddress(string accountId, string personId, Domain.Tutors.Models.ValueObjects.Address address, CancellationToken cancellationToken)
    {
        try
        {
            var personUpdateOptions = new PersonUpdateOptions
            {
                Address = new AddressOptions
                {
                    State = address.State,
                    City = address.City,
                    Line1 = address.LineOne,
                    Line2 = address.LineTwo,
                    PostalCode = address.PostalCode.ToString()
                }
            };

            var personService = new PersonService();

            await personService.UpdateAsync(accountId, personId, personUpdateOptions, cancellationToken: cancellationToken);

            return Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }

    public async Task<Result> UpdateBankAccount(string accountId, Domain.Tutors.Models.ValueObjects.BankAccount bankAccount, CancellationToken cancellationToken)
    {
        try
        {
            var externalAccountCreateOptions = new ExternalAccountCreateOptions
            {
                ExternalAccount = new AccountBankAccountOptions
                {
                    AccountHolderName = bankAccount.HolderFullName,
                    AccountHolderType = bankAccount.HolderType,
                    AccountNumber = bankAccount.Iban,
                    Country = "BG",
                    Currency = "BGN"
                },
                DefaultForCurrency = true
            };

            var externalAccountService = new ExternalAccountService();
            await externalAccountService.CreateAsync(accountId, externalAccountCreateOptions, cancellationToken: cancellationToken);

            return Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }

    public async Task<Result> UpdateVerificationDocuments(string accountId, string personId, Document identityVerificationDocumentFront, Document identityVerificationDocumentBack, Document addressVerificationDocument, CancellationToken cancellationToken)
    {
        try
        {
            var personService = new PersonService();

            var personUpdateOptions = new PersonUpdateOptions
            {
                Verification = new PersonVerificationOptions
                {
                    Document = new PersonVerificationDocumentOptions
                    {
                        Front = identityVerificationDocumentFront.ExternalId,
                        Back = identityVerificationDocumentBack.ExternalId
                    },
                    AdditionalDocument = new PersonVerificationAdditionalDocumentOptions
                    {
                        Front = addressVerificationDocument.ExternalId
                    }
                }
            };

            await personService.UpdateAsync(accountId, personId, personUpdateOptions, cancellationToken: cancellationToken);

            return Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);

        }
    }

    public async Task<Result> UpdateTermsOfService(string accountId, TermsOfService termsOfService, CancellationToken cancellationToken)
    {
        try
        {
            var accountUpdateOptions = new AccountUpdateOptions
            {
                TosAcceptance = new AccountTosAcceptanceOptions
                {
                    ServiceAgreement = termsOfService.Type,
                    Date = termsOfService.DateOfAcceptance,
                    Ip = termsOfService.IpOfAcceptance
                }
            };

            var accountService = new AccountService();

            await accountService.UpdateAsync(accountId, accountUpdateOptions, cancellationToken: cancellationToken);

            return Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }

    public async Task<Result<(string fileId, string fileName, string fileUrl)>> UploadIdentityDocument(Stream identityDocument, CancellationToken cancellationToken)
    {
        try
        {
            var fileService = new FileService();

            var fileCreateOptions = new FileCreateOptions
            {
                File = identityDocument,
                Purpose = FilePurpose.IdentityDocument
            };

            var file = await fileService.CreateAsync(fileCreateOptions, cancellationToken: cancellationToken);

            return Result.Ok((file.Id, file.Title, file.Url));
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }
}
