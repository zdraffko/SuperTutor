using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.UpdateAbout.Validators;

internal class NewAboutValidator : ICommandValidator<UpdateTutorProfileAboutCommand>
{
    public Result Validate(UpdateTutorProfileAboutCommand command)
    {
        var newAboutInvariant = new TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant(command.NewAbout);
        if (newAboutInvariant.IsBroken())
        {
            return Result.Fail(newAboutInvariant.ErrorMessage);
        }

        return Result.Ok();
    }
}
