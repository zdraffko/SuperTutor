using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Repositories;

namespace SuperTutor.Contexts.Catalog.Application.Students.Queries.GetAllfavoriteFilters;

public class GetAllfavoriteFiltersForStudentQueryHandler : IQueryHandler<GetAllfavoriteFiltersForStudentQuery, GetAllfavoriteFiltersForStudentQueryPayload>
{
    private readonly IQueryRepository queryRepository;

    public GetAllfavoriteFiltersForStudentQueryHandler(IQueryRepository queryRepository) => this.queryRepository = queryRepository;

    public async Task<Result<GetAllfavoriteFiltersForStudentQueryPayload>> Handle(GetAllfavoriteFiltersForStudentQuery query, CancellationToken cancellationToken)
    {
        var databaseQuery = "select Filter from catalog.FavoriteFilters where StudentId = @StudentId";
        var filters = await queryRepository.GetAll<string>(databaseQuery, new { StudentId = query.StudentId.Value.ToString() }, cancellationToken);
        var payload = new GetAllfavoriteFiltersForStudentQueryPayload(filters);

        return Result.Ok(payload);
    }
}
