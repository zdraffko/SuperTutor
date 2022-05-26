namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreTermsOfServiceAccepted;

public class GetTutorAreTermsOfServiceAcceptedQueryPayload
{
    public GetTutorAreTermsOfServiceAcceptedQueryPayload(bool areTermsOfServiceAccepted) => AreTermsOfServiceAccepted = areTermsOfServiceAccepted;

    public bool AreTermsOfServiceAccepted { get; }
}
