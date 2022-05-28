namespace SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorProfilesByFilter;

public class GetTutorProfilesByFilterRequest
{
    public GetTutorProfilesByFilterRequest(
        int? tutoringSubject,
        IEnumerable<int>? tutoringGrades,
        decimal? minRateForOneHour,
        decimal? maxRateForOneHour)
    {
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        MinRateForOneHour = minRateForOneHour;
        MaxRateForOneHour = maxRateForOneHour;
    }

    public int? TutoringSubject { get; }

    public IEnumerable<int>? TutoringGrades { get; }

    public decimal? MinRateForOneHour { get; }

    public decimal? MaxRateForOneHour { get; }
}
