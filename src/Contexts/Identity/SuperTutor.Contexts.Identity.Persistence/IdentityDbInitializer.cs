using Microsoft.EntityFrameworkCore;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Initializers;

namespace SuperTutor.Contexts.Identity.Persistence;

public class IdentityDbInitializer : DefaultDbInitializer
{
    public IdentityDbInitializer(IdentityDbContext dbContext) : base(dbContext)
    {
    }
}
