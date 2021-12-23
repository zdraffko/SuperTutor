using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.IncreaseRateForOneHour;

internal class IncreaseProfileRateForOneHourCommandHandler : ICommandHandler<IncreaseProfileRateForOneHourCommand>
{
    private readonly IProfileRepository profileRepository;

    public IncreaseProfileRateForOneHourCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(IncreaseProfileRateForOneHourCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.IncreaseRateForOneHour(command.IncreaseAmount);

        return Result.Ok();
    }
}
