using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create.Validators;

internal class StudyGradeValidator : ICommandValidator<CreateStudentProfileCommand>
{
    public Result Validate(CreateStudentProfileCommand command)
    {
        var studyGrade = Enumeration.FromValue<Grade>(command.StudyGrade);
        if (studyGrade is null)
        {
            return Result.Fail($"A study grade with value '{command.StudyGrade}' does not exist");
        }

        return Result.Ok();
    }
}
