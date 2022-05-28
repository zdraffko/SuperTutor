using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;

public class GetTutorProfilesByFilterQuery : Query<GetTutorProfilesByFilterQueryPayload>
{
    public GetTutorProfilesByFilterQuery(
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
