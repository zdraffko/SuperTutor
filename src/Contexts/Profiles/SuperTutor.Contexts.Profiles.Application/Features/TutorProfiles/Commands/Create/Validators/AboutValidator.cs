using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create.Validators;

internal class AboutValidator : ICommandValidator<CreateTutorProfileCommand, CreateTutorProfileCommandPayload>
{
    public Result Validate(CreateTutorProfileCommand command)
    {
        var aboutInvariant = new TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant(command.About);
        if (aboutInvariant.IsBroken())
        {
            return Result.Fail(aboutInvariant.ErrorMessage);
        }

        return Result.Ok();
    }
}
