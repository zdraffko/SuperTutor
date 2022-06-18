namespace SuperTutor.ApiGateways.Admin.Models.Profiles.GetAllTutorProfilesForReview;

public class GetAllTutorProfilesForReviewResponse
{
    public IEnumerable<TutorProfile> TutorProfiles { get; set; }

    public class TutorProfile
    {
        public Guid Id { get; set; }

        public string About { get; set; }

        public string TutoringSubject { get; set; }

        public IEnumerable<string> TutoringGrades { get; set; }

        public decimal RateForOneHour { get; set; }

        public string Status { get; set; }
    }
}
