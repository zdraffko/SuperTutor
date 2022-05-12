using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Queries.GetIdentityInfo;

internal class GetUserIdentityInfoQueryHandler : IQueryHandler<GetUserIdentityInfoQuery, GetUserIdentityInfoQueryPayload>
{
    private readonly IUserService userService;

    public GetUserIdentityInfoQueryHandler(IUserService userService) => this.userService = userService;

    public async Task<Result<GetUserIdentityInfoQueryPayload>> Handle(GetUserIdentityInfoQuery query, CancellationToken cancellationToken)
    {
        var user = await userService.GetById(query.UserId);
        if (user == null)
        {
            return Result.Fail("User not found");
        }

        var queryPayload = new GetUserIdentityInfoQueryPayload(user.Email, user.Type);

        return Result.Ok(queryPayload);
    }
}
