using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.RemoveStudySubjects.Validators;

internal class StudySubjectsForRemovalValidator : ICommandValidator<RemoveStudySubjectsFromStudentProfileCommand>
{
    public Result Validate(RemoveStudySubjectsFromStudentProfileCommand command)
    {
        var studySubjectsForRemoval = Enumeration.FromValues<Subject>(command.StudySubjectsForRemoval).ToHashSet();
        if (!studySubjectsForRemoval.Any())
        {
            return Result.Fail("At least one study subject must be selected for removal");
        }

        return Result.Ok();
    }
}
