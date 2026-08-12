using DocumentManagementSystem.Domain.Documents.ValueObjects;
using FluentAssertions;

namespace DocumentManagementSystem.Domain.Tests.Documents.ValueObjects;

public class FileSizeTests
{
    [Fact]
    public void Create_ShouldCreateFileSize_WhenValueIsPositive()
    {
        var fileSize = FileSize.Create(1024);

        fileSize.Value.Should().Be(1024);
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsZero()
    {
        var action = () => FileSize.Create(0);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("*greater than zero*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsNegative()
    {
        var action = () => FileSize.Create(-1);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("*greater than zero*");
    }

    [Fact]
    public void Megabytes_ShouldReturnCorrectValue()
    {
        var fileSize = FileSize.Create(2 * 1024 * 1024);

        fileSize.Megabytes.Should().Be(2);
    }

    [Fact]
    public void EqualFileSizes_ShouldBeEqual()
    {
        var first = FileSize.Create(1024);
        var second = FileSize.Create(1024);

        first.Should().Be(second);
    }

    [Fact]
    public void DifferentFileSizes_ShouldNotBeEqual()
    {
        var first = FileSize.Create(1024);
        var second = FileSize.Create(2048);

        first.Should().NotBe(second);
    }

    [Fact]
    public void ToString_ShouldReturnByteValue()
    {
        var fileSize = FileSize.Create(1024);

        fileSize.ToString().Should().Be("1024");
    }
}
