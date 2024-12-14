using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthModuleSpu.Infrastructure.Configurations;

public class NotificationReceiverConfiguration : IEntityTypeConfiguration<NotificationReceiver>
{
    public void Configure(EntityTypeBuilder<NotificationReceiver> builder)
    {
        builder.ToTable("notification_receivers");
        
        builder.Property(entity => entity.NotificationId)
            .HasColumnName("notification_id")
            .HasColumnType("bigint");

        builder.Property(entity => entity.UserId)
            .HasColumnName("user_id")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(entity => entity.IsRead)
            .HasColumnName("is_read")
            .HasColumnType("bool")
            .HasDefaultValue(false)
            .IsRequired();
        
        builder.Property(entity => entity.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp");
        
        builder.HasKey(entity => new { entity.NotificationId, entity.UserId });

        builder.HasOne(entity => entity.Notification)
            .WithMany(entity => entity.NotificationReceivers)
            .HasForeignKey(entity => entity.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(entity => entity.User)
            .WithMany(entity => entity.NotificationReceivers)
            .HasForeignKey(entity => entity.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}