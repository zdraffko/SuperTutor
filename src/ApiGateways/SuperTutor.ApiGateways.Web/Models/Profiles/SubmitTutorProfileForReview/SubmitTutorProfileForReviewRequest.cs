namespace SuperTutor.ApiGateways.Web.Models.Profiles.SubmitTutorProfileForReview;

public class SubmitTutorProfileForReviewRequest
{
    public SubmitTutorProfileForReviewRequest(Guid tutorProfileId) => TutorProfileId = tutorProfileId;

    public Guid TutorProfileId { get; }
}
