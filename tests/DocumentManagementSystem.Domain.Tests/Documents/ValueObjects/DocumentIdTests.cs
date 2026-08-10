using DocumentManagementSystem.Domain.Documents.ValueObjects;
using FluentAssertions;

namespace DocumentManagementSystem.Domain.Tests.Documents.ValueObjects;

public class DocumentIdTests
{
    [Fact]
    public void New_ShouldCreateNonEmptyId()
    {
        var documentId = DocumentId.New();

        documentId.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void New_ShouldCreateUniqueIds()
    {
        var first = DocumentId.New();
        var second = DocumentId.New();

        first.Should().NotBe(second);
    }

    [Fact]
    public void Empty_ShouldReturnEmptyId()
    {
        var documentId = DocumentId.Empty();

        documentId.Value.Should().Be(Guid.Empty);
    }

    [Fact]
    public void DocumentIdsWithSameValue_ShouldBeEqual()
    {
        var value = Guid.NewGuid();

        var first = new DocumentId(value);
        var second = new DocumentId(value);

        first.Should().Be(second);
    }

    [Fact]
    public void ToString_ShouldReturnGuidValue()
    {
        var value = Guid.NewGuid();
        var documentId = new DocumentId(value);

        documentId.ToString().Should().Be(value.ToString());
    }
}
