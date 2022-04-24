using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;

public class GetTutorProfilesByFilterQuery : Query<GetTutorProfilesByFilterQueryPayload>
{
    public GetTutorProfilesByFilterQuery(
        IEnumerable<int> tutoringSubjects,
        IEnumerable<int> tutoringGrades,
        decimal minRateForOneHour,
        decimal maxRateForOneHour)
    {
        TutoringSubjects = tutoringSubjects;
        TutoringGrades = tutoringGrades;
        MinRateForOneHour = minRateForOneHour;
        MaxRateForOneHour = maxRateForOneHour;
    }

    public IEnumerable<int> TutoringSubjects { get; }

    public IEnumerable<int> TutoringGrades { get; }

    public decimal MinRateForOneHour { get; }

    public decimal MaxRateForOneHour { get; }
}
