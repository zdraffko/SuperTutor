using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Profiles.CreateTutorProfile;

public class CreateTutorProfileResponse
{
    [JsonPropertyName("tutorProfileId")]
    public Guid TutorProfileId { get; init; }
}
