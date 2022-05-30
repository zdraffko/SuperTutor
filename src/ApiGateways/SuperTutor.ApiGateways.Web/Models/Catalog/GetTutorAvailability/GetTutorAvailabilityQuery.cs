namespace SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorAvailability;

public class GetTutorAvailabilityQuery
{
    public GetTutorAvailabilityQuery(Guid tutorId) => TutorId = tutorId;

    public Guid TutorId { get; }
}
