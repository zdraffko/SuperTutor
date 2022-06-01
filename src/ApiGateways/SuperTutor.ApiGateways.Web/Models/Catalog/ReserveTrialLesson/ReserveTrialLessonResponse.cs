using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Catalog.ReserveTrialLesson;

public class ReserveTrialLessonResponse
{
    [JsonPropertyName("lessonId")]
    public Guid? LessonId { get; set; }
}
