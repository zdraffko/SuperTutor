using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Queries.GetIdentityInfo;

public class GetUserIdentityInfoQuery : Query<GetUserIdentityInfoQueryPayload>
{
    public GetUserIdentityInfoQuery(Guid userId) => UserId = userId;

    public Guid UserId { get; }
}
