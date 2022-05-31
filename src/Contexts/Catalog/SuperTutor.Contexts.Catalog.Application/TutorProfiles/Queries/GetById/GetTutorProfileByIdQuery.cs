using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetById;

public class GetTutorProfileByIdQuery : Query<GetTutorProfileByIdQueryPayload>
{
    public TutorProfileId TutorProfileId { get; set; }
}
