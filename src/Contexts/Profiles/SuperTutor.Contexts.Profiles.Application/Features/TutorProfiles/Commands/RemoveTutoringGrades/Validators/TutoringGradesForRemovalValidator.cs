using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RemoveTutoringGrades.Validators;

internal class TutoringGradesForRemovalValidator : ICommandValidator<RemoveTutoringGradesFromTutorProfileCommand>
{
    public Result Validate(RemoveTutoringGradesFromTutorProfileCommand command)
    {
        var tutoringGradesForRemoval = Enumeration.FromValues<Grade>(command.TutoringGradesForRemoval).ToHashSet();
        if (!tutoringGradesForRemoval.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected for removal");
        }

        return Result.Ok();
    }
}
