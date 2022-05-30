using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Schedule.GetTutorTimeSlotsForWeek;

public class GetTutorTimeSlotsForWeekResponse
{
    [JsonPropertyName("timeSlotsForWeek")]
    public IEnumerable<TimeSlot> TimeSlotsForWeek { get; init; }

    public class TimeSlot
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("tutorId")]
        public Guid TutorId { get; init; }

        [JsonPropertyName("date")]
        public DateOnly Date { get; init; }

        [JsonPropertyName("startTime")]
        public TimeOnly StartTime { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; }
    }
}
