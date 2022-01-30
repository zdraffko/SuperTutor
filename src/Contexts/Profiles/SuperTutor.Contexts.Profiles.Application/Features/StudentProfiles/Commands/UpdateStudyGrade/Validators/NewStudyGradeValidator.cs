using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.UpdateStudyGrade.Validators;

internal class NewStudyGradeValidator : ICommandValidator<UpdateStudyGradeForStudentProfileCommand>
{
    public Result Validate(UpdateStudyGradeForStudentProfileCommand command)
    {
        var newStudyGrade = Enumeration.FromValue<Grade>(command.NewStudyGrade);
        if (newStudyGrade is null)
        {
            return Result.Fail($"A study grade with value '{command.NewStudyGrade}' does not exist.");
        }

        return Result.Ok();
    }
}
