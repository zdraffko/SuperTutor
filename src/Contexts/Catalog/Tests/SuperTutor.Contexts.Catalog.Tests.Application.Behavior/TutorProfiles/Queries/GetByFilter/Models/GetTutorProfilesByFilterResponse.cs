using System.Text.Json.Serialization;

namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.TutorProfiles.Queries.GetByFilter.Models;

public class GetTutorProfilesByFilterResponse
{
    [JsonPropertyName("tutorProfiles")]
    public IEnumerable<TutorProfile>? TutorProfiles { get; set; }

    public class TutorProfile
    {
        [JsonPropertyName("about")]
        public string About { get; set; } = default!;

        [JsonPropertyName("tutoringSubject")]
        public string TutoringSubject { get; set; } = default!;

        [JsonPropertyName("tutoringGrades")]
        public IEnumerable<string> TutoringGrades { get; set; } = default!;

        [JsonPropertyName("rateForOneHour")]
        public decimal RateForOneHour { get; set; } = default!;
    }
}
