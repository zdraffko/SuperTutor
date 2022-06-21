namespace SuperTutor.ApiGateways.Web.Models.Profiles.DeleteTutorProfile;

public class DeleteTutorProfileRequest
{
    public DeleteTutorProfileRequest(Guid tutorProfileId) => TutorProfileId = tutorProfileId;

    public Guid TutorProfileId { get; }
}
