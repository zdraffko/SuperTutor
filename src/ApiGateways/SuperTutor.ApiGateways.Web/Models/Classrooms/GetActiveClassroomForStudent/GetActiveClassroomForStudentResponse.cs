using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Classrooms.GetActiveClassroomForStudent;

public class GetActiveClassroomForStudentResponse
{
    [JsonPropertyName("classroomId")]
    public Guid? ClassroomId { get; set; }
}
