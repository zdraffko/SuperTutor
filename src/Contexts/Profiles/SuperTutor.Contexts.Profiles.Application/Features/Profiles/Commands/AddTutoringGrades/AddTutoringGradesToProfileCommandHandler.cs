using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.AddTutoringGrades;

internal class AddTutoringGradesToProfileCommandHandler : ICommandHandler<AddTutoringGradesToProfileCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public AddTutoringGradesToProfileCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(AddTutoringGradesToProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new TutorProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        var newTutoringGrades = Enumeration.FromValues<TutoringGrade>(command.NewTutoringGrades).ToHashSet();
        if (!newTutoringGrades.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected to be added.");
        }

        profile.AddTutoringGrades(newTutoringGrades);

        return Result.Ok();
    }
}
