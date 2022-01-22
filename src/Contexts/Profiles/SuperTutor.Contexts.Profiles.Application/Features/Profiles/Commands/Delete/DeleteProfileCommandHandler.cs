using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Delete;

internal class DeleteProfileCommandHandler : ICommandHandler<DeleteProfileCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public DeleteProfileCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new TutorProfileId(command.ProfileId), cancellationToken);
        if (profile == null)
        {
            return Result.Fail("Profile not found.");
        }

        profileRepository.Remove(profile);

        return Result.Ok();
    }
}
