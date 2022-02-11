using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.AddStudySubjects.Validators;

internal class NewStudySubjectsValidator : ICommandValidator<AddStudySubjectsToStudentProfileCommand>
{
    public Result Validate(AddStudySubjectsToStudentProfileCommand command)
    {
        var newStudySubjects = Enumeration.FromValues<Subject>(command.NewStudySubjects).ToHashSet();
        if (!newStudySubjects.Any())
        {
            return Result.Fail("At least one new study subject must be selected to be added");
        }

        return Result.Ok();
    }
}
