namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.TutorProfiles.Queries.GetByFilter.Models;

internal class GetTutorProfilesByFilterFilter
{
    public IEnumerable<int> TutoringSubjects { get; set; } = default!;

    public IEnumerable<int> TutoringGrades { get; set; } = default!;

    public decimal MinRateForOneHour { get; set; } = default!;

    public decimal MaxRateForOneHour { get; set; } = default!;
}
