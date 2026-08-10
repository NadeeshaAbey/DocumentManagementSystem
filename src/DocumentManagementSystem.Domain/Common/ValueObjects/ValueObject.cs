using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Domain.Common.ValueObjects;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        if(other is null || other.GetType() != GetType())
        {
            return false;
        }

        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueObject);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(
            0,
            (current, component) =>
                HashCode.Combine(current, component));
    }

    public static bool operator == (ValueObject? left, ValueObject? right)
    {
        if(ReferenceEquals(left, right))
        {
            return true;
        }

        if(left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}
