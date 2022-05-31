using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorProfileById;

public class GetTutorProfileByIdResponse
{
    [JsonPropertyName("profile")]
    public TutorProfile Profile { get; init; }

    public class TutorProfile
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("tutorId")]
        public Guid TutorId { get; init; }

        [JsonPropertyName("tutorFirstName")]
        public string TutorFirstName { get; init; }

        [JsonPropertyName("tutorLastName")]
        public string TutorLastName { get; init; }

        [JsonPropertyName("about")]
        public string About { get; init; }

        [JsonPropertyName("tutoringSubject")]
        public string TutoringSubject { get; init; }

        [JsonPropertyName("tutoringGrades")]
        public IEnumerable<string> TutoringGrades { get; init; }

        [JsonPropertyName("rateForOneHour")]
        public decimal RateForOneHour { get; init; }
    }
}
