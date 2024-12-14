using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthModuleSpu.Infrastructure.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");
        
        builder.Property(entity => entity.Id)
            .HasColumnName("id")
            .HasColumnType("bigint");

        builder.Property(entity => entity.Name)
            .HasColumnName("name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(entity => entity.RequestedMetrics)
            .HasColumnName("requested_metrics")
            .HasColumnType("jsonb")
            .IsRequired();
        
        builder.Property(entity => entity.Report)
            .HasColumnName("report")
            .HasColumnType("jsonb");

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("now()");

        builder.HasIndex(entity => entity.Name)
            .IsUnique();

        builder.HasKey(entity => entity.Id);

        builder.HasMany(entity => entity.JobPermissions)
            .WithOne(entity => entity.Job)
            .HasForeignKey(entity => entity.JobId);

        builder.HasMany(entity => entity.JobAttachments)
            .WithOne(entity => entity.Job)
            .HasForeignKey(entity => entity.JobId);

        builder.HasOne(entity => entity.QueueJob)
            .WithOne(entity => entity.Job)
            .HasForeignKey<QueueJob>(entity => entity.JobId);
    }
}