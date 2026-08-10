using DocumentManagementSystem.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace DocumentManagementSystem.Domain.Tests.Common.ValueObjects;

public class ValueObjectTests
{
    [Fact]
    public void Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        var first = new TestValueObject("test");
        var second = new TestValueObject("test");

        first.Equals(second).Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenValuesAreDiffernet()
    {
        var first = new TestValueObject("test");
        var second = new TestValueObject("different");

        first.Equals(second).Should().BeFalse();
    }

    [Fact]
    public void EqualityOperator_ShouldReturnTrue_WhenValuesAreEqual()
    {
        var first = new TestValueObject("test");
        var second = new TestValueObject("test");

        (first == second).Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_ShouldReturnFalse_WhenValuesAreDifferent()
    {
        var first = new TestValueObject("test");
        var second = new TestValueObject("different");

        (first == second).Should().BeFalse();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenOtherObjectIsNull()
    {
        var valueObject = new TestValueObject("test");

        valueObject.Equals(null).Should().BeFalse();
    }


    private sealed class TestValueObject(string value) : ValueObject
    {
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return value;
        }
    }
}
