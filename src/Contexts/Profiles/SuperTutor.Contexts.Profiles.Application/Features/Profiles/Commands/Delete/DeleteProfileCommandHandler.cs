using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Delete;

internal class DeleteProfileCommandHandler : ICommandHandler<DeleteProfileCommand>
{
    private readonly IProfileRepository profileRepository;

    public DeleteProfileCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile == null)
        {
            return Result.Fail("Profile not found.");
        }

        profileRepository.Remove(profile);

        return Result.Ok();
    }
}
