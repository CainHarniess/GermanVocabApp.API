using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username)
               .HasMaxLength(50);

        builder.Property(u => u.Password)
               .HasMaxLength(50);
    }
}
