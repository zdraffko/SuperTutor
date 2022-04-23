using Microsoft.AspNetCore.Mvc;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.ModelBinders;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;

public class FromJsonQueryAttribute : ModelBinderAttribute
{
    public FromJsonQueryAttribute() => BinderType = typeof(JsonQueryModelBinder);
}
