using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForReview;

public class GetAllTutorProfilesForReviewQueryPayload
{
    public GetAllTutorProfilesForReviewQueryPayload(IEnumerable<TutorProfile> tutorProfiles) => TutorProfiles = tutorProfiles;

    public IEnumerable<TutorProfile> TutorProfiles { get; }

    public class TutorProfile
    {
        public TutorProfile(TutorProfileId id, string about, string tutoringSubject, IEnumerable<string> tutoringGrades, decimal rateForOneHour, string status)
        {
            Id = id;
            About = about;
            TutoringSubject = tutoringSubject;
            TutoringGrades = tutoringGrades;
            RateForOneHour = rateForOneHour;
            Status = status;
        }

        public TutorProfileId Id { get; }

        public string About { get; }

        public string TutoringSubject { get; }

        public IEnumerable<string> TutoringGrades { get; }

        public decimal RateForOneHour { get; }

        public string Status { get; }
    }
}
