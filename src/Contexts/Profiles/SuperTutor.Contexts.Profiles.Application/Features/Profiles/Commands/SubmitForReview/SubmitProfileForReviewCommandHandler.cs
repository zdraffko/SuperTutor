using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.SubmitForReview;

public class SubmitProfileForReviewCommandHandler : ICommandHandler<SubmitProfileForReviewCommand>
{
    public Task<Result> Handle(SubmitProfileForReviewCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
