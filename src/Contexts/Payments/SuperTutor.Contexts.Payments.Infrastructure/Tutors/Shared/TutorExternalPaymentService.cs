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
            var options = new PersonUpdateOptions
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

            var service = new PersonService();

            await service.UpdateAsync(accountId, personId, options, cancellationToken: cancellationToken);

            return Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Fail(exception.Message);
        }
    }
}
