using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperTutor.Contexts.Identity.Domain.Users;
using SuperTutor.Contexts.Identity.Domain.Users.Models.Enumerations;

namespace SuperTutor.Contexts.Identity.Infrastructure.Persistence.EntityTypeConfigurations;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Type).IsRequired().HasConversion(new EnumToNumberConverter<UserType, int>());
        builder.Property(user => user.Email).IsRequired();
        builder.Property(user => user.NormalizedEmail).IsRequired();
        builder.Property(user => user.UserName).IsRequired();
        builder.Property(user => user.NormalizedUserName).IsRequired();
        builder.Property(user => user.FirstName).IsRequired();
        builder.Property(user => user.LastName).IsRequired();
        builder.Property(user => user.PasswordHash).IsRequired();
        builder.Property(user => user.SecurityStamp).IsRequired();
        builder.Property(user => user.ConcurrencyStamp).IsRequired();
    }
}
