using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthModuleSpu.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        
        builder.Property(entity => entity.Id)
            .HasColumnName("id")
            .HasColumnType("bigint");

        builder.Property(entity => entity.Username)
            .HasColumnName("username")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(entity => entity.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("now()");

        builder.Property(entity => entity.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("timestamp");

        builder.HasIndex(entity => entity.Email)
            .IsUnique();
        
        builder.HasIndex(entity => entity.Username)
            .IsUnique();

        builder.HasKey(entity => entity.Id);

        builder.HasMany(entity => entity.JobPermissions)
            .WithOne(entity => entity.User)
            .HasForeignKey(entity => entity.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(entity => entity.NotificationReceivers)
            .WithOne(entity => entity.User)
            .HasForeignKey(entity => entity.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}