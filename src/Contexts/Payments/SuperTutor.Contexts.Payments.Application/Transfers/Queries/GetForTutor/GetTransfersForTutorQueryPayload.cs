using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Shared.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Transfers;
using SuperTutor.Contexts.Payments.Domain.Tutors;

namespace SuperTutor.Contexts.Payments.Application.Transfers.Queries.GetForTutor;

public class GetTransfersForTutorQueryPayload
{
    public GetTransfersForTutorQueryPayload(IEnumerable<Transfer> transfers) => Transfers = transfers;

    public IEnumerable<Transfer> Transfers { get; }

    public class Transfer
    {
        public TransferId Id { get; init; }

        public ChargeId ChargeId { get; init; }

        public LessonId LessonId { get; init; }

        public StudentId StudentId { get; init; }

        public TutorId TutorId { get; init; }

        public decimal Amount { get; init; }

        public string Currency { get; init; }
    }
}
