using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentManagementSystem.Domain.Documents.ValueObjects;

namespace DocumentManagementSystem.Domain.Tests.Documents.ValueObjects;

public class ContentTypeTests
{
    [Fact]
    public void Create_ShouldCreateContentType_WhenTypeIsSupported()
    {
        var contentType = ContentType.Create("application/pdf");

        contentType.Value.Should().Be("application/pdf");
    }

    [Fact]
    public void Create_ShouldNormalizeContentType()
    {
        var contentType = ContentType.Create(" Application/PDF ");

        contentType.Value.Should().Be("application/pdf");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsEmpty()
    {
        var action = () => ContentType.Create(string.Empty);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*cannot be empty*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsWhitespace()
    {
        var action = () => ContentType.Create("   ");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*cannot be empty*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenTypeIsNotSupported()
    {
        var action = () => ContentType.Create("application/x-custom-format");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*not supported*");
    }

    [Theory]
    [InlineData("application/pdf")]
    [InlineData("application/msword")]
    [InlineData("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
    [InlineData("application/vnd.ms-excel")]
    [InlineData("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
    [InlineData("text/plain")]
    [InlineData("image/png")]
    [InlineData("image/jpeg")]
    public void Create_ShouldAcceptSupportedTypes(string value)
    {
        var contentType = ContentType.Create(value);

        contentType.Value.Should().Be(value);
    }

    [Fact]
    public void EqualContentTypes_ShouldBeEqual()
    {
        var first = ContentType.Create("application/pdf");
        var second = ContentType.Create("application/pdf");

        first.Should().Be(second);
    }

    [Fact]
    public void DifferentContentTypes_ShouldNotBeEqual()
    {
        var first = ContentType.Create("application/pdf");
        var second = ContentType.Create("image/png");

        first.Should().NotBe(second);
    }
}
