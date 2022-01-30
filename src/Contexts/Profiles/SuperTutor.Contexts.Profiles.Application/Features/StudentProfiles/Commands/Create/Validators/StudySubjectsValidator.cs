using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create.Validators;

internal class StudySubjectsValidator : ICommandValidator<CreateStudentProfileCommand>
{
    public Result Validate(CreateStudentProfileCommand command)
    {
        var studySubjects = Enumeration.FromValues<Subject>(command.StudySubjects).ToHashSet();
        if (!studySubjects.Any())
        {
            return Result.Fail("At least one study subject must be selected.");
        }

        return Result.Ok();
    }
}
