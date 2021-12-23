using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RemoveTutoringGrades;

internal class RemoveTutoringGradesFromProfileCommandHandler : ICommandHandler<RemoveTutoringGradesFromProfileCommand>
{
    private readonly IProfileRepository profileRepository;

    public RemoveTutoringGradesFromProfileCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(RemoveTutoringGradesFromProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        var tutoringGradesForRemoval = Enumeration.FromValues<TutoringGrade>(command.TutoringGradesForRemoval).ToHashSet();
        if (!tutoringGradesForRemoval.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected for removal.");
        }

        profile.RemoveTutoringGrades(tutoringGradesForRemoval);

        return Result.Ok();
    }
}
