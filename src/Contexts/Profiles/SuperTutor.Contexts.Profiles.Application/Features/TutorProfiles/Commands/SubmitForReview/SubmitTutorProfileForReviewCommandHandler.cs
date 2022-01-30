using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

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
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        tutorProfile.SubmitForReview();

        return Result.Ok();
    }
}
