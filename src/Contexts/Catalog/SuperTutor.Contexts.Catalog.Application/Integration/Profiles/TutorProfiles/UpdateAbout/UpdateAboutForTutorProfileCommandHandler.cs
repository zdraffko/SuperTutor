using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateAbout;

internal class UpdateAboutForTutorProfileCommandHandler : ICommandHandler<UpdateAboutForTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public UpdateAboutForTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository) => this.tutorProfileRepository = tutorProfileRepository;

    public async Task<Result> Handle(UpdateAboutForTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.UpdateAbout(command.NewAbout);

        return Result.Ok();
    }
}
