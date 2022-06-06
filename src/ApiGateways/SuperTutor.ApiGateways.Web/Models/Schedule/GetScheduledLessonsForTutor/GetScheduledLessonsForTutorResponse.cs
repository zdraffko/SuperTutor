using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Schedule.GetScheduledLessonsForTutor;

public class GetScheduledLessonsForTutorResponse
{
    [JsonPropertyName("scheduledLessons")]
    public IEnumerable<ScheduledLesson> ScheduledLessons { get; init; }

    public class ScheduledLesson
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("tutorId")]
        public Guid TutorId { get; init; }

        [JsonPropertyName("studentId")]
        public Guid StudentId { get; init; }

        [JsonPropertyName("date")]
        public DateOnly Date { get; init; }

        [JsonPropertyName("startTime")]
        public TimeOnly StartTime { get; init; }

        [JsonPropertyName("duration")]
        public TimeSpan Duration { get; init; }

        [JsonPropertyName("subject")]
        public string Subject { get; init; }

        [JsonPropertyName("grade")]
        public string Grade { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; }

        [JsonPropertyName("paymentStatus")]
        public string PaymentStatus { get; init; }
    }
}
