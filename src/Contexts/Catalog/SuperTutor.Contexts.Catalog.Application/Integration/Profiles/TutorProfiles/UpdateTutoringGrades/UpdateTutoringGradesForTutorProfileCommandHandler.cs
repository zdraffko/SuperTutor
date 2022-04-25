using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateTutoringGrades;

internal class UpdateTutoringGradesForTutorProfileCommandHandler : ICommandHandler<UpdateTutoringGradesForTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public UpdateTutoringGradesForTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository) => this.tutorProfileRepository = tutorProfileRepository;

    public async Task<Result> Handle(UpdateTutoringGradesForTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.UpdateTutoringGrades(command.NewTutoringGrades.ToList());

        return Result.Ok();
    }
}
