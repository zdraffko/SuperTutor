using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Delete;

public class DeleteTutorProfileCommand : Command
{
    public DeleteTutorProfileCommand(TutorProfileId tutorProfileId) => TutorProfileId = tutorProfileId;

    public TutorProfileId TutorProfileId { get; }
}
