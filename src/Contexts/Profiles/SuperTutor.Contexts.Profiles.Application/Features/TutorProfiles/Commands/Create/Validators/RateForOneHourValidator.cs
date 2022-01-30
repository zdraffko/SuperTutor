using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create.Validators;

internal class RateForOneHourValidator : ICommandValidator<CreateTutorProfileCommand>
{
    public Result Validate(CreateTutorProfileCommand command)
    {
        var rateForOneHourInvariant = new TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(command.RateForOneHour);
        if (rateForOneHourInvariant.IsBroken())
        {
            return Result.Fail(rateForOneHourInvariant.ErrorMessage);
        }

        return Result.Ok();
    }
}
