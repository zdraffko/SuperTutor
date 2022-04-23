using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.StudentProfiles;

public class StudentProfileDeletedIntegrationEvent : IntegrationEvent
{
    public StudentProfileDeletedIntegrationEvent(Guid studentId) => StudentId = studentId;

    public Guid StudentId { get; }
}
