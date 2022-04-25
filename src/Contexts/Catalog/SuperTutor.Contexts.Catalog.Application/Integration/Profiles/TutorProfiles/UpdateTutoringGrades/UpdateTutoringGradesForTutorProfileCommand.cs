using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateTutoringGrades;

public class UpdateTutoringGradesForTutorProfileCommand : Command
{
    public UpdateTutoringGradesForTutorProfileCommand(TutorProfileId tutorProfileId, IEnumerable<TutoringGrade> newTutoringGrades)
    {
        TutorProfileId = tutorProfileId;
        NewTutoringGrades = newTutoringGrades;
    }

    public TutorProfileId TutorProfileId { get; }

    public IEnumerable<TutoringGrade> NewTutoringGrades { get; }
}
