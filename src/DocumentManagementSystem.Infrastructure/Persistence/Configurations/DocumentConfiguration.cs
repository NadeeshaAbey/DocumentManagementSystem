using DocumentManagementSystem.Domain.Documents.Entities;
using DocumentManagementSystem.Domain.Documents.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManagementSystem.Infrastructure.Persistence.Configurations;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(document => document.Id);

        builder.Property(document => document.Id)
            .HasConversion(
                id => id.Value,
                value => new(value))
            .ValueGeneratedNever();

        builder.Property(document => document.FileName)
            .HasConversion(
                fileName => fileName.Value,
                value => FileName.Create(value))
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(document => document.ContentType)
            .HasConversion(
                contentType => contentType.Value,
                value => ContentType.Create(value))
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(document => document.FileSize)
            .HasConversion(
                fileSize => fileSize.Value,
                value => FileSize.Create(value))
            .IsRequired();

        builder.Property(document => document.StorageKey)
            .HasConversion(
                storageKey => storageKey.Value,
                value => StorageKey.Create(value))
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(document => document.UploadedAt)
            .IsRequired();

    }
}
