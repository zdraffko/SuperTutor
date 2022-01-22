using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.SubmitForReview;

internal class SubmitTutorProfileForReviewCommandHandler : ICommandHandler<SubmitTutorProfileForReviewCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public SubmitTutorProfileForReviewCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(SubmitTutorProfileForReviewCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(new TutorProfileId(command.TutorProfileId), cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        tutorProfile.SubmitForReview();

        return Result.Ok();
    }
}
