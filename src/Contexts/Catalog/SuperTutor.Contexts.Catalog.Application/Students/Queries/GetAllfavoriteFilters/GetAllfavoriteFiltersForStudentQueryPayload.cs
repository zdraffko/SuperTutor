namespace SuperTutor.Contexts.Catalog.Application.Students.Queries.GetAllfavoriteFilters;

public class GetAllFavoriteFiltersForStudentQueryPayload
{
    public GetAllFavoriteFiltersForStudentQueryPayload(IEnumerable<string> filters) => Filters = filters;

    public IEnumerable<string> Filters { get; }
}
