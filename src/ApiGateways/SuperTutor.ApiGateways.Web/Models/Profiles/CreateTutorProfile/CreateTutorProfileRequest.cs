namespace SuperTutor.ApiGateways.Web.Models.Profiles.CreateTutorProfile;

public class CreateTutorProfileRequest
{
    public CreateTutorProfileRequest(string about, int tutoringSubject, IEnumerable<int> tutoringGrades, decimal rateForOneHour)
    {
        About = about;
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
    }

    public string About { get; }

    public int TutoringSubject { get; }

    public IEnumerable<int> TutoringGrades { get; }

    public decimal RateForOneHour { get; }
}
