using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.IncreaseRateForOneHour;

internal class IncreaseTutorProfileRateForOneHourCommandHandler : ICommandHandler<IncreaseTutorProfileRateForOneHourCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public IncreaseTutorProfileRateForOneHourCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(IncreaseTutorProfileRateForOneHourCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        tutorProfile.IncreaseRateForOneHour(command.IncreaseAmount);

        return Result.Ok();
    }
}
