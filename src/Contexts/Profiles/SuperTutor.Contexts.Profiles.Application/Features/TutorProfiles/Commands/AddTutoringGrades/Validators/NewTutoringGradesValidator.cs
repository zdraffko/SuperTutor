using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.AddTutoringGrades.Validators;

internal class NewTutoringGradesValidator : ICommandValidator<AddTutoringGradesToTutorProfileCommand>
{
    public Result Validate(AddTutoringGradesToTutorProfileCommand command)
    {
        var newTutoringGrades = Enumeration.FromValues<Grade>(command.NewTutoringGrades).ToHashSet();
        if (!newTutoringGrades.Any())
        {
            return Result.Fail("At least one new tutoring grade must be selected to be added");
        }

        return Result.Ok();
    }
}