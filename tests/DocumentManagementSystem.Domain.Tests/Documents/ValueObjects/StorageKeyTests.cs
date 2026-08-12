using DocumentManagementSystem.Domain.Documents.ValueObjects;
using FluentAssertions;

namespace DocumentManagementSystem.Domain.Tests.Documents.ValueObjects;

public class StorageKeyTests
{
    [Fact]
    public void Create_ShouldCreateStorageKey_WhenValueIsValid()
    {
        var storageKey = StorageKey.Create("documents/abc123.pdf");

        storageKey.Value.Should().Be("documents/abc123.pdf");
    }

    [Fact]
    public void Create_ShouldTrimWhitespace()
    {
        var storageKey = StorageKey.Create("  documents/abc123.pdf  ");

        storageKey.Value.Should().Be("documents/abc123.pdf");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsEmpty()
    {
        var action = () => StorageKey.Create(string.Empty);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*cannot be empty*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsWhitespace()
    {
        var action = () => StorageKey.Create("   ");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*cannot be empty*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueExceedsMaximumLength()
    {
        var value = new string('a', 256);

        var action = () => StorageKey.Create(value);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*255 characters*");
    }

    [Theory]
    [InlineData("../document.pdf")]
    [InlineData("documents/../document.pdf")]
    [InlineData("..\\document.pdf")]
    [InlineData("documents/../../document.pdf")]
    [InlineData("documents\\..\\document.pdf")]
    public void Create_ShouldThrow_WhenValueContainsParentDirectorySegment(
    string value)
    {
        var action = () => StorageKey.Create(value);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*parent directory segments*");
    }

    [Theory]
    [InlineData("/document.pdf")]
    [InlineData("\\document.pdf")]
    [InlineData("C:\\documents\\document.pdf")]
    [InlineData("C:/documents/document.pdf")]
    public void Create_ShouldThrow_WhenValueIsAbsolutePath(
    string value)
    {
        var action = () => StorageKey.Create(value);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*absolute path*");
    }

    [Theory]
    [InlineData("documents/report..final.pdf")]
    [InlineData("documents/...report.pdf")]
    [InlineData("documents/report...pdf")]
    public void Create_ShouldAllowDoubleDots_WhenNotParentDirectorySegment(
    string value)
    {
        var storageKey = StorageKey.Create(value);

        storageKey.Value.Should().Be(value);
    }

    [Fact]
    public void EqualStorageKeys_ShouldBeEqual()
    {
        var first = StorageKey.Create("documents/abc123.pdf");
        var second = StorageKey.Create("documents/abc123.pdf");

        first.Should().Be(second);
    }

    [Fact]
    public void DifferentStorageKeys_ShouldNotBeEqual()
    {
        var first = StorageKey.Create("documents/abc123.pdf");
        var second = StorageKey.Create("documents/xyz789.pdf");

        first.Should().NotBe(second);
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        var storageKey = StorageKey.Create("documents/abc123.pdf");

        storageKey.ToString().Should().Be("documents/abc123.pdf");
    }
} 
