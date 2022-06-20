namespace SuperTutor.ApiGateways.Admin.Models.Profiles.RequestTutorProfileRedaction;

public class RequestTutorProfileRedactionRequest
{
    public Guid TutorProfileId { get; set; }

    public string Comment { get; set; }
}
