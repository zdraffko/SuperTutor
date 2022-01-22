using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RequestRedaction;

internal class RequestTutorProfileRedactionCommandHandler : ICommandHandler<RequestTutorProfileRedactionCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public RequestTutorProfileRedactionCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(RequestTutorProfileRedactionCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(new TutorProfileId(command.TutorProfileId), cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        var redactionComment = new RedactionComment(tutorProfile.Id, new AdminId(command.AdminId), command.Comment);
        tutorProfile.RequestRedaction(redactionComment);

        return Result.Ok();
    }
}
