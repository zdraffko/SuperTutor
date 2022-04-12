using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Persistence.Contexts;
using SuperTutor.Contexts.Profiles.Persistence.Contexts.Contracts;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<ProfilesDbContext>()
        .As<DbContext>()
        .As<ITutorProfilesDbContext>()
        .As<IStudentProfilesDbContext>()
        .InstancePerLifetimeScope();
}
