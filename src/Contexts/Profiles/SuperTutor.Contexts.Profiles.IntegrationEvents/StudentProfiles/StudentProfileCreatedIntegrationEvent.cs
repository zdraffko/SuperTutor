using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.IntegrationEvents.StudentProfiles;

public class StudentProfileCreatedIntegrationEvent : IntegrationEvent
{
    public StudentProfileCreatedIntegrationEvent(Guid studentId) => StudentId = studentId;

    public Guid StudentId { get; }
}
