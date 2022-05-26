using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.AcceptTermsOfService;

public class AcceptTutorTermsOfServiceCommand : Command
{
    public AcceptTutorTermsOfServiceCommand(TutorId tutorId, string ipOfAcceptance)
    {
        TutorId = tutorId;
        IpOfAcceptance = ipOfAcceptance;
    }

    public TutorId TutorId { get; }

    public string IpOfAcceptance { get; }
}
