using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Contracts;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public class Profile : Entity<ProfileId, int>, IAggregateRoot
{
    private string subject;

    private string status;

    public Profile(ProfileId id) : base(id)
    {
    }

    public void SubmitForReview()
    {

    }
}
