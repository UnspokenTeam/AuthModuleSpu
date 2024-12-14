using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthModuleSpu.Infrastructure.Configurations;

public class JobPermissionConfiguration : IEntityTypeConfiguration<JobPermission>
{
    public void Configure(EntityTypeBuilder<JobPermission> builder)
    {
        builder.ToTable("job_permissions");
        
        builder.Property(entity => entity.UserId)
            .HasColumnName("user_id")
            .HasColumnType("bigint");

        builder.Property(entity => entity.JobId)
            .HasColumnName("job_id")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(entity => entity.Permissions)
            .HasColumnName("permissions")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(entity => entity.UserType)
            .HasColumnName("user_type")
            .HasColumnType("user_to_task_type")
            .HasConversion(
                v => v.ToString(),
                v => (UserToTaskType)Enum.Parse(typeof(UserToTaskType), v) // Convert string to enum
            )
            .IsRequired();
        
        builder.HasKey(entity => new { entity.JobId, entity.UserId });

        builder.HasOne(entity => entity.Job)
            .WithMany(entity => entity.JobPermissions)
            .HasForeignKey(entity => entity.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(entity => entity.User)
            .WithMany(entity => entity.JobPermissions)
            .HasForeignKey(entity => entity.JobId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
    }
}