using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthModuleSpu.Infrastructure.Configurations;

public class QueueJobConfiguration : IEntityTypeConfiguration<QueueJob>
{
    public void Configure(EntityTypeBuilder<QueueJob> builder)
    {
        builder.ToTable("queue");
        
        builder.Property(entity => entity.Id)
            .HasColumnName("id")
            .HasColumnType("bigint");

        builder.Property(entity => entity.Progress)
            .HasColumnName("progress")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("now()");
        
        builder.HasKey(entity => entity.Id);

        builder.HasOne(entity => entity.Job)
            .WithOne(entity => entity.QueueJob)
            .HasForeignKey<QueueJob>(entity => entity.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}