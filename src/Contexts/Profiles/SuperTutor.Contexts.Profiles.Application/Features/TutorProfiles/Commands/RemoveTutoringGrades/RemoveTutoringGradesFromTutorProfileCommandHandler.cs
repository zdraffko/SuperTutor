using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RemoveTutoringGrades;

internal class RemoveTutoringGradesFromTutorProfileCommandHandler : ICommandHandler<RemoveTutoringGradesFromTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public RemoveTutoringGradesFromTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(RemoveTutoringGradesFromTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(new TutorProfileId(command.TutorProfileId), cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        var tutoringGradesForRemoval = Enumeration.FromValues<TutoringGrade>(command.TutoringGradesForRemoval).ToHashSet();
        if (!tutoringGradesForRemoval.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected for removal.");
        }

        tutorProfile.RemoveTutoringGrades(tutoringGradesForRemoval);

        return Result.Ok();
    }
}
