using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthModuleSpu.Infrastructure.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications");
        
        builder.Property(entity => entity.Id)
            .HasColumnName("id")
            .HasColumnType("bigint");

        builder.Property(entity => entity.Text)
            .HasColumnName("text")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(entity => entity.JobId)
            .HasColumnName("job_id")
            .HasColumnType("bigint")
            .IsRequired();
        
        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("now()");
        
        builder.HasKey(entity => entity.Id);

        builder.HasMany(entity => entity.NotificationReceivers)
            .WithOne(entity => entity.Notification)
            .HasForeignKey(entity => entity.NotificationId);

        builder.HasOne(entity => entity.Job)
            .WithMany(entity => entity.Notifications)
            .HasForeignKey(entity => entity.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}