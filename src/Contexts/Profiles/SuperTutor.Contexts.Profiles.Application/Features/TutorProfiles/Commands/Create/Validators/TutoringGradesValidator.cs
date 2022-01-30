using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create.Validators;

internal class TutoringGradesValidator : ICommandValidator<CreateTutorProfileCommand>
{
    public Result Validate(CreateTutorProfileCommand command)
    {
        var tutoringGrades = Enumeration.FromValues<Grade>(command.TutoringGrades).ToHashSet();
        if (!tutoringGrades.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected.");
        }

        return Result.Ok();
    }
}
