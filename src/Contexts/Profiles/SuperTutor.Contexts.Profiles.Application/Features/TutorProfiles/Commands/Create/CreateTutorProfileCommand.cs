﻿using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create;

public class CreateTutorProfileCommand : Command
{
    public CreateTutorProfileCommand(Guid tutorId, string about, int tutoringSubject, IEnumerable<int> tutoringGrades, decimal rateForOneHour)
    {
        TutorId = tutorId;
        About = about;
        TutoringSubject = tutoringSubject;
        TutoringGrades = tutoringGrades;
        RateForOneHour = rateForOneHour;
    }

    public Guid TutorId { get; }

    public string About { get; }

    public int TutoringSubject { get; }

    public IEnumerable<int> TutoringGrades { get; }

    public decimal RateForOneHour { get; }
}
