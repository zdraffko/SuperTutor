namespace SuperTutor.ApiGateways.Web.Models.Profiles.UpdateTutorProfileAbout;

public class UpdateTutorProfileAboutRequest
{
    public UpdateTutorProfileAboutRequest(Guid tutorProfileId, string newAbout)
    {
        TutorProfileId = tutorProfileId;
        NewAbout = newAbout;
    }

    public Guid TutorProfileId { get; }

    public string NewAbout { get; }
}
