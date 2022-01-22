using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RequestRedaction;

internal class RequestProfileRedactionCommandHandler : ICommandHandler<RequestProfileRedactionCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public RequestProfileRedactionCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(RequestProfileRedactionCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new TutorProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        var redactionComment = new RedactionComment(profile.Id, new AdminId(command.AdminId), command.Comment);
        profile.RequestRedaction(redactionComment);

        return Result.Ok();
    }
}
