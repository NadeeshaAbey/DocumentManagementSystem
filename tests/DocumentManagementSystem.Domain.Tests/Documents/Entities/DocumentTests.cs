using DocumentManagementSystem.Domain.Documents.Entities;
using DocumentManagementSystem.Domain.Documents.ValueObjects;
using FluentAssertions;

namespace DocumentManagementSystem.Domain.Tests.Documents.Entities;

public class DocumentTests
{
    [Fact]
    public void Create_ShouldCreateDocument_WhenValuesAreValid()
    {
        var fileName = FileName.Create("invoice.pdf");
        var contentType = ContentType.Create("application/pdf");
        var fileSize = FileSize.Create(1024);
        var storageKey = StorageKey.Create("documents/test.pdf");
        var uploadedAt = DateTimeOffset.UtcNow;

        var document = Document.Create(
            fileName,
            contentType,
            fileSize,
            storageKey,
            uploadedAt);

        document.Id.Value.Should().NotBe(Guid.Empty);
        document.FileName.Should().Be(fileName);
        document.ContentType.Should().Be(contentType);
        document.FileSize.Should().Be(fileSize);
        document.StorageKey.Should().Be(storageKey);
        document.UploadedAt.Should().Be(uploadedAt);
    }

    [Fact]
    public void Create_ShouldGenerateUniqueIds()
    {
        var first = CreateDocument();
        var second = CreateDocument();

        first.Id.Should().NotBe(second.Id);
    }

    [Fact]
    public void Create_ShouldPreserveUploadedAt()
    {
        var uploadedAt = new DateTimeOffset(
            2026,
            8,
            12,
            10,
            30,
            0,
            TimeSpan.Zero);

        var document = Document.Create(
            FileName.Create("invoice.pdf"),
            ContentType.Create("application/pdf"),
            FileSize.Create(1024),
            StorageKey.Create("documents/invoice.pdf"),
            uploadedAt);

        document.UploadedAt.Should().Be(uploadedAt);
    }

    private static Document CreateDocument()
    {
        return Document.Create(
            FileName.Create("invoice.pdf"),
            ContentType.Create("application/pdf"),
            FileSize.Create(1024),
            StorageKey.Create("documents/invoice.pdf"),
            DateTimeOffset.UtcNow);
    }
}
