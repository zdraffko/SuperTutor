using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorAvailability;

public class GetTutorAvailabilityResponse
{
    [JsonPropertyName("availabilities")]
    public IEnumerable<Availability> Availabilities { get; init; }

    public class Availability
    {
        [JsonPropertyName("date")]
        public DateOnly Date { get; init; }

        [JsonPropertyName("hours")]
        public IEnumerable<TimeOnly> Hours { get; init; }
    }
}
