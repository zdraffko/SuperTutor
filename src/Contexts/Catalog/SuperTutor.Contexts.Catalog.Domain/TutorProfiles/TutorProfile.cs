using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Catalog.Domain.TutorProfiles;

public class TutorProfile : Entity<TutorProfileId, Guid>, IAggregateRoot
{
    private readonly List<int> tutoringGrades;

    public TutorProfile(TutorProfileId id, string about, int tutoringSubject, List<int> tutoringGrades, decimal rateForOneHour, bool isActive) : base(id)
    {
        About = about[..TutorProfileConstants.AboutMaxLength];
        TutoringSubject = tutoringSubject;
        this.tutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
        IsActive = isActive;
    }

    public string About { get; }

    public int TutoringSubject { get; }

    public IReadOnlyCollection<int> TutoringGrades => tutoringGrades;

    public decimal RateForOneHour { get; }

    public bool IsActive { get; }
}
