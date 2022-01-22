using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.IncreaseRateForOneHour;

internal class IncreaseProfileRateForOneHourCommandHandler : ICommandHandler<IncreaseProfileRateForOneHourCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public IncreaseProfileRateForOneHourCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(IncreaseProfileRateForOneHourCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new TutorProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.IncreaseRateForOneHour(command.IncreaseAmount);

        return Result.Ok();
    }
}
