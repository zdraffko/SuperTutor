using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Validation.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RequestRedaction.Validators;

internal class CommentValidator : ICommandValidator<RequestTutorProfileRedactionCommand>
{
    public Result Validate(RequestTutorProfileRedactionCommand command)
    {
        var commentContentInvariant = new TutorProfileRedactionCommentContentMustNotBeEmptyOrAboveTheMaxLenghtInvariant(command.Comment);
        if (commentContentInvariant.IsBroken())
        {
            return Result.Fail(commentContentInvariant.ErrorMessage);
        }

        return Result.Ok();
    }
}
