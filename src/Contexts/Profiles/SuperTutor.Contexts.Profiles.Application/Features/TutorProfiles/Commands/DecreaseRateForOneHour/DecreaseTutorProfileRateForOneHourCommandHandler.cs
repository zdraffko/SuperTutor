using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.DecreaseRateForOneHour;

internal class DecreaseTutorProfileRateForOneHourCommandHandler : ICommandHandler<DecreaseTutorProfileRateForOneHourCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public DecreaseTutorProfileRateForOneHourCommandHandler(ITutorProfileRepository tutorProfileRepository) => this.tutorProfileRepository = tutorProfileRepository;

    public async Task<Result> Handle(DecreaseTutorProfileRateForOneHourCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.DecreaseRateForOneHour(command.DecreaseAmount);

        return Result.Ok();
    }
}
