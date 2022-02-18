using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Delete;

internal class DeleteTutorProfileCommandHandler : ICommandHandler<DeleteTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public DeleteTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository) => this.tutorProfileRepository = tutorProfileRepository;

    public async Task<Result> Handle(DeleteTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfileRepository.Remove(tutorProfile);

        return Result.Ok();
    }
}
