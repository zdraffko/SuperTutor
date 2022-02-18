using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.DecreaseRateForOneHour.Validators;

internal class DecreaseAmountValidator : ICommandValidator<DecreaseTutorProfileRateForOneHourCommand>
{
    public Result Validate(DecreaseTutorProfileRateForOneHourCommand command)
    {
        var decreaseAmountInvariant = new TutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant(command.DecreaseAmount);
        if (decreaseAmountInvariant.IsBroken())
        {
            return Result.Fail(decreaseAmountInvariant.ErrorMessage);
        }

        return Result.Ok();
    }
}
