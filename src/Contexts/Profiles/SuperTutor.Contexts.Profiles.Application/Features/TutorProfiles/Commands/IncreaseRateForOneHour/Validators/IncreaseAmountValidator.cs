using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.IncreaseRateForOneHour.Validators;

internal class IncreaseAmountValidator : ICommandValidator<IncreaseTutorProfileRateForOneHourCommand>
{
    public Result Validate(IncreaseTutorProfileRateForOneHourCommand command)
    {
        var increaseAmountInvariant = new TutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant(command.IncreaseAmount);
        if (increaseAmountInvariant.IsBroken())
        {
            return Result.Fail(increaseAmountInvariant.ErrorMessage);
        }

        return Result.Ok();
    }
}
