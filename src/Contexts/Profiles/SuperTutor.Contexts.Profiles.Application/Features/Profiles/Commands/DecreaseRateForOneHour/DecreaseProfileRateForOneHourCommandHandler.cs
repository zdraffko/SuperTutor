using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.DecreaseRateForOneHour;

internal class DecreaseProfileRateForOneHourCommandHandler : ICommandHandler<DecreaseProfileRateForOneHourCommand>
{
    private readonly IProfileRepository profileRepository;

    public DecreaseProfileRateForOneHourCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(DecreaseProfileRateForOneHourCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.DecreaseRateForOneHour(command.DecreaseAmount);

        return Result.Ok();
    }
}
