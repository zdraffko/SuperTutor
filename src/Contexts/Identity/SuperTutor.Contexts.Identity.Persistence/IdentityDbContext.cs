using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Identity.Persistence.Entities;

namespace SuperTutor.Contexts.Identity.Persistence;

public class IdentityDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRoleClaim<int>>(roleClaimBuilder => { roleClaimBuilder.ToTable("RoleClaims"); });
        modelBuilder.Entity<IdentityRole<int>>(roleBuilder => { roleBuilder.ToTable(name: "Roles"); });
        modelBuilder.Entity<IdentityUserClaim<int>>(claimBuilder => { claimBuilder.ToTable("UserClaims"); });
        modelBuilder.Entity<IdentityUserLogin<int>>(userLoginBuilder => { userLoginBuilder.ToTable("UserLogins"); });
        modelBuilder.Entity<IdentityUserRole<int>>(userRoleBuilder => { userRoleBuilder.ToTable("UserRoles"); });
        modelBuilder.Entity<User>(userBuilder => { userBuilder.ToTable(name: "Users"); });
        modelBuilder.Entity<IdentityUserToken<int>>(userTokenBuilder => { userTokenBuilder.ToTable("UserTokens"); });
    }
}
