using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Domain.Tutors;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;

public class GetTutorProfilesByFilterQueryPayload
{
    public GetTutorProfilesByFilterQueryPayload(IEnumerable<TutorProfile> tutorProfiles) => TutorProfiles = tutorProfiles;

    public IEnumerable<TutorProfile> TutorProfiles { get; }

    public class TutorProfile
    {
        public TutorProfile(
            TutorProfileId id,
            TutorId tutorId,
            string tutorFirstName,
            string tutorLastName,
            string about,
            string tutoringSubject,
            IEnumerable<string> tutoringGrades,
            decimal rateForOneHour)
        {
            Id = id;
            TutorId = tutorId;
            TutorFirstName = tutorFirstName;
            TutorLastName = tutorLastName;
            About = about;
            TutoringSubject = tutoringSubject;
            TutoringGrades = tutoringGrades;
            RateForOneHour = rateForOneHour;
        }

        public TutorProfileId Id { get; }

        public TutorId TutorId { get; }

        public string TutorFirstName { get; }

        public string TutorLastName { get; }

        public string About { get; }

        public string TutoringSubject { get; }

        public IEnumerable<string> TutoringGrades { get; }

        public decimal RateForOneHour { get; }
    }
}
