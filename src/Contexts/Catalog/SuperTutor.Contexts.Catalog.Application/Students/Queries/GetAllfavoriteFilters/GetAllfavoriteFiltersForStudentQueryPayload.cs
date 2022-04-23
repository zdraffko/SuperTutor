namespace SuperTutor.Contexts.Catalog.Application.Students.Queries.GetAllfavoriteFilters;

public class GetAllfavoriteFiltersForStudentQueryPayload
{
    public GetAllfavoriteFiltersForStudentQueryPayload(IEnumerable<string> filters) => Filters = filters;

    public IEnumerable<string> Filters { get; }
}
