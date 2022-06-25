using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Classrooms.GetActiveClassroomForTutor;

public class GetActiveClassroomForTutorResponse
{
    [JsonPropertyName("classroomId")]
    public Guid? ClassroomId { get; set; }
}
