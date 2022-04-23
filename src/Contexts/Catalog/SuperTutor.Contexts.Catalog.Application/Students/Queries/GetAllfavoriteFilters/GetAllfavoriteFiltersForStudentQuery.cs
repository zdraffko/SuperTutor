using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Application.Students.Queries.GetAllfavoriteFilters;

public class GetAllfavoriteFiltersForStudentQuery : Query<GetAllfavoriteFiltersForStudentQueryPayload>
{
    public GetAllfavoriteFiltersForStudentQuery(StudentId studentId) => StudentId = studentId;

    public StudentId StudentId { get; }
}
