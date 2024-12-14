using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthModuleSpu.Infrastructure.Configurations;

public class JobAttachmentConfiguration : IEntityTypeConfiguration<JobAttachment>
{
    public void Configure(EntityTypeBuilder<JobAttachment> builder)
    {
        builder.ToTable("job_attachments");
        
        builder.Property(entity => entity.Id)
            .HasColumnName("id")
            .HasColumnType("bigint");

        builder.Property(entity => entity.JobId)
            .HasColumnName("job_id")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(entity => entity.S3FileName)
            .HasColumnName("s3_file_name")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(entity => entity.S3BucketName)
            .HasColumnName("s3_bucket_name")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(entity => entity.Type)
            .HasColumnName("s3_bucket_name")
            .HasColumnType("attachment_type")
            .HasConversion(
                v => v.ToString(),
                v => (AttachmentType)Enum.Parse(typeof(AttachmentType), v) // Convert string to enum
            )
            .IsRequired();

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("now()");

        builder
            .HasIndex(entity => new { entity.Job, entity.S3FileName, entity.S3BucketName })
            .IsUnique();

        builder.HasKey(entity => entity.Id);

        builder.HasOne(entity => entity.Job)
            .WithMany(entity => entity.JobAttachments)
            .HasForeignKey(entity => entity.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}