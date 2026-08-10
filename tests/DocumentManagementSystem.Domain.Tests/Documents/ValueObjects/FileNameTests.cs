using DocumentManagementSystem.Domain.Documents.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Domain.Tests.Documents.ValueObjects;

public class FileNameTests
{
    [Fact]
    public void Create_ShouldCreateFileName_WhenValueIsValid()
    {
        var fileName = FileName.Create("invoice.pdf");

        fileName.Value.Should().Be("invoice.pdf");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsEmpty()
    {
        var action = () => FileName.Create(string.Empty);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*cannot be empty*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsWhitespace()
    {
        var action = () => FileName.Create("   ");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*cannot be empty*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenFileNameExceedsMaximumLength()
    {
        var fileName = new string('a', 252) + ".pdf";

        var action = () => FileName.Create(fileName);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*255 characters*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenFileNameContainsDirectorySeparator()
    {
        var action = () => FileName.Create("../invoice.pdf");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*directory separators*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenFileNameHasNoExtension()
    {
        var action = () => FileName.Create("invoice");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*extension*");
    }

    [Fact]
    public void Create_ShouldPreserveOriginalFileName()
    {
        var fileName = FileName.Create("My Invoice 2026.pdf");

        fileName.Value.Should().Be("My Invoice 2026.pdf");
    }

    [Fact]
    public void EqualFileNames_ShouldBeEqual()
    {
        var first = FileName.Create("invoice.pdf");
        var second = FileName.Create("invoice.pdf");

        first.Should().Be(second);
    }

    [Fact]
    public void DifferentFileNames_ShouldNotBeEqual()
    {
        var first = FileName.Create("invoice.pdf");
        var second = FileName.Create("contract.pdf");

        first.Should().NotBe(second);
    }
}
