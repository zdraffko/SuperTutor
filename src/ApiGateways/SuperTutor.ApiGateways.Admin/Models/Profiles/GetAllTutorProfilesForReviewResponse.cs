using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Admin.Models.Profiles;

public class GetAllTutorProfilesForReviewResponse
{
    [JsonPropertyName("tutorProfiles")]
    public IEnumerable<TutorProfile> TutorProfiles { get; init; }
}

public class TutorProfile
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("about")]
    public string About { get; init; }

    [JsonPropertyName("tutoringSubject")]
    public string TutoringSubject { get; init; }

    [JsonPropertyName("tutoringGrades")]
    public IEnumerable<string> TutoringGrades { get; init; }

    [JsonPropertyName("rateForOneHour")]
    public decimal RateForOneHour { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }
}
