using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RequestRedaction;

internal class RequestProfileRedactionCommandHandler : ICommandHandler<RequestProfileRedactionCommand>
{
    private readonly IProfileRepository profileRepository;

    public RequestProfileRedactionCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(RequestProfileRedactionCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.RequestRedaction();

        return Result.Ok();
    }
}
