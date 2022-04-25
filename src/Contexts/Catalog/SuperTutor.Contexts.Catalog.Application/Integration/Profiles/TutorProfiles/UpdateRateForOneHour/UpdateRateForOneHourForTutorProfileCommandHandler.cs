using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateRateForOneHour;

internal class UpdateRateForOneHourForTutorProfileCommandHandler : ICommandHandler<UpdateRateForOneHourForTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public UpdateRateForOneHourForTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository) => this.tutorProfileRepository = tutorProfileRepository;

    public async Task<Result> Handle(UpdateRateForOneHourForTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.UpdateRateForOneHour(command.NewRateForOneHour);

        return Result.Ok();
    }
}
