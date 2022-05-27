using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create.Validators;

internal class TutoringSubjectValidator : ICommandValidator<CreateTutorProfileCommand, CreateTutorProfileCommandPayload>
{
    public Result Validate(CreateTutorProfileCommand command)
    {
        var tutoringSubject = Enumeration.FromValue<Subject>(command.TutoringSubject);
        if (tutoringSubject is null)
        {
            return Result.Fail($"A tutoring subject with value '{command.TutoringSubject}' does not exist");
        }

        return Result.Ok();
    }
}
