using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.GetTransfersForTutor;

public class GetTransfersForTutorResponse
{
    [JsonPropertyName("transfers")]
    public IEnumerable<Transfer> Transfers { get; init; }

    public class Transfer
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("chargeId")]
        public Guid ChargeId { get; init; }

        [JsonPropertyName("lessonId")]
        public Guid LessonId { get; init; }

        [JsonPropertyName("studentId")]
        public Guid StudentId { get; init; }

        [JsonPropertyName("tutorId")]
        public Guid TutorId { get; init; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; }
    }
}
