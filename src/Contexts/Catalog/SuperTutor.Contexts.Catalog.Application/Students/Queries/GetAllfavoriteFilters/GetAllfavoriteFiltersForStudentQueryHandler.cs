using FluentResults;
using SuperTutor.Contexts.Catalog.Application.Students.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Application.Students.Queries.GetAllfavoriteFilters;

public class GetAllFavoriteFiltersForStudentQueryHandler : IQueryHandler<GetAllFavoriteFiltersForStudentQuery, GetAllFavoriteFiltersForStudentQueryPayload>
{
    private readonly IStudentsQueryRepository studentsQueryRepository;

    public GetAllFavoriteFiltersForStudentQueryHandler(IStudentsQueryRepository studentsQueryRepository) => this.studentsQueryRepository = studentsQueryRepository;

    public async Task<Result<GetAllFavoriteFiltersForStudentQueryPayload>> Handle(GetAllFavoriteFiltersForStudentQuery query, CancellationToken cancellationToken)
    {
        var filters = await studentsQueryRepository.GetAllFiltersForStudent(query.StudentId, cancellationToken);
        var payload = new GetAllFavoriteFiltersForStudentQueryPayload(filters);

        return Result.Ok(payload);
    }
}
