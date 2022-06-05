using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForReview;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForTutor;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.Shared;

public interface ITutorProfilesQueryRepository
{
    Task<IEnumerable<GetAllTutorProfilesForTutorQueryPayload.TutorProfile>> GetAllForTutor(TutorId tutorId, CancellationToken cancellationToken);

    Task<IEnumerable<GetAllTutorProfilesForReviewQueryPayload.TutorProfile>> GetAllForReview(CancellationToken cancellationToken);
}
