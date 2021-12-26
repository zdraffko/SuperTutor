using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Identity.Persistence.Entities;

namespace SuperTutor.Contexts.Identity.Persistence;

public class IdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("identity");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(roleClaimBuilder => { roleClaimBuilder.ToTable("RoleClaims"); });
        modelBuilder.Entity<IdentityRole<Guid>>(roleBuilder => { roleBuilder.ToTable(name: "Roles"); });
        modelBuilder.Entity<IdentityUserClaim<Guid>>(claimBuilder => { claimBuilder.ToTable("UserClaims"); });
        modelBuilder.Entity<IdentityUserLogin<Guid>>(userLoginBuilder => { userLoginBuilder.ToTable("UserLogins"); });
        modelBuilder.Entity<IdentityUserRole<Guid>>(userRoleBuilder => { userRoleBuilder.ToTable("UserRoles"); });
        modelBuilder.Entity<User>(userBuilder => { userBuilder.ToTable(name: "Users"); });
        modelBuilder.Entity<IdentityUserToken<Guid>>(userTokenBuilder => { userTokenBuilder.ToTable("UserTokens"); });
    }
}
