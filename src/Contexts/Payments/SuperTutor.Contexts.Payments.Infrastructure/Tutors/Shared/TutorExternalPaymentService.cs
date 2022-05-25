using FluentResults;
using Stripe;
using SuperTutor.Contexts.Payments.Application.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Shared;

internal class TutorExternalPaymentService : ITutorExternalPaymentService
{
    public async Task<Result<(string accountId, string personId)>> CreateAccount(UserId userId, string email, CancellationToken cancellationToken)
    {
        try
        {
            var accountCreateOptions = new AccountCreateOptions
            {
                Type = "custom",
                Country = "BG",
                Email = email,
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
                    Email = email
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
                    { "UserId", userId.Value.ToString() }
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
}
