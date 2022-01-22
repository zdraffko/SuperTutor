using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.UpdateAbout;

internal class UpdateTutorProfileAboutCommandHandler : ICommandHandler<UpdateTutorProfileAboutCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public UpdateTutorProfileAboutCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(UpdateTutorProfileAboutCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(new TutorProfileId(command.TutorProfileId), cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        tutorProfile.UpdateAbout(command.NewAbout);

        return Result.Ok();
    }
}
