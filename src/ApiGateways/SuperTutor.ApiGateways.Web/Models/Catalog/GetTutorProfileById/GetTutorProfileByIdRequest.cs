namespace SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorProfileById;

public class GetTutorProfileByIdRequest
{
    public GetTutorProfileByIdRequest(Guid tutorProfileId) => TutorProfileId = tutorProfileId;

    public Guid TutorProfileId { get; }
}
