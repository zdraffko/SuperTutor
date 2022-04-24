namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;

public class GetTutorProfilesByFilterQueryPayload
{
    public GetTutorProfilesByFilterQueryPayload(IEnumerable<TutorProfile> tutorProfiles) => TutorProfiles = tutorProfiles;

    public IEnumerable<TutorProfile> TutorProfiles { get; }

    public class TutorProfile
    {
        public TutorProfile(string about, string tutoringSubject, IEnumerable<string> tutoringGrades, decimal rateForOneHour)
        {
            About = about;
            TutoringSubject = tutoringSubject;
            TutoringGrades = tutoringGrades;
            RateForOneHour = rateForOneHour;
        }

        public string About { get; }

        public string TutoringSubject { get; }

        public IEnumerable<string> TutoringGrades { get; }

        public decimal RateForOneHour { get; }
    }
}
