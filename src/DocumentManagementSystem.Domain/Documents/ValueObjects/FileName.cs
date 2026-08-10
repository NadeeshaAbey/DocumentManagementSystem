using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Domain.Documents.ValueObjects;

public sealed record FileName
{
    private const int MaxLength = 255;
    public string Value { get; }

    private FileName(string value)
    {
        Value = value;
    }

    public static FileName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "File name cannot be empty.",
                nameof(value));
        }

        if(value.Length > MaxLength)
        {
            throw new ArgumentException(
                $"File name cannot exceed {MaxLength} characters",
                nameof(value));
        }

        if(value.Contains('/') || value.Contains('\\'))
        {
            throw new ArgumentException(
                "File name cannot contain directory separators.",
                nameof(value));
        }

        if(value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            throw new ArgumentException(
                "File name contains invalid characters.",
                nameof(value));
        }

        if (string.IsNullOrWhiteSpace(Path.GetExtension(value)))
        {
            throw new ArgumentException(
                "File name must have an extension.",
                nameof(value));
        }

        return new FileName(value);
    }

    public override string ToString()
    {
        return Value;
    }
}
