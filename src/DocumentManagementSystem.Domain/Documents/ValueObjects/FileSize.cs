using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Domain.Documents.ValueObjects;

public readonly record struct FileSize
{
    public long Value { get; }

    private FileSize(long value)
    {
        Value = value;
;   }

    public static FileSize Create(long value)
    {
        if(value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                "File size must be greater than zero.");
        }

        return new FileSize(value);
    }

    public double Megabytes => Value / 1024d / 1024d;

    public override string ToString()
    {
        return Value.ToString();
    }
}
