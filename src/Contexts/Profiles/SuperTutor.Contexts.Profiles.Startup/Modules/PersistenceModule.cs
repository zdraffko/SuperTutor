using Autofac;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Persistence.Contexts;
using SuperTutor.Contexts.Profiles.Persistence.Contexts.Contracts;
using SuperTutor.Contexts.Profiles.Persistence.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.UnitOfWork;
using SuperTutor.SharedLibraries.BuildingBlocks.Persistence.Services;

namespace SuperTutor.Contexts.Profiles.Startup.Modules;

internal class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterDbComponents(builder);
        RegisterRepositories(builder);
    }

    private static void RegisterDbComponents(ContainerBuilder builder)
    {
        builder.RegisterType<ProfilesDbContext>()
            .As<DbContext>()
            .As<ITutorProfilesDbContext>()
            .As<IStudentProfilesDbContext>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }

    private static void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<TutorProfileRepository>().As<ITutorProfileRepository>().InstancePerLifetimeScope();
        builder.RegisterType<StudentProfileRepository>().As<IStudentProfileRepository>().InstancePerLifetimeScope();
    }
}
