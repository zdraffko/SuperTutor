using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Profiles.GetAllTutorProfilesForTutor;

public class GetAllTutorProfilesForTutorResponse
{
    [JsonPropertyName("tutorProfiles")]
    public IEnumerable<TutorProfile> TutorProfiles { get; init; }

    public class TutorProfile
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("about")]
        public string About { get; init; }

        [JsonPropertyName("tutoringSubject")]
        public int TutoringSubject { get; init; }

        [JsonPropertyName("tutoringGrades")]
        public IEnumerable<int> TutoringGrades { get; init; }

        [JsonPropertyName("rateForOneHour")]
        public decimal RateForOneHour { get; init; }
    }
}
