using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Domain.Documents.ValueObjects;

public sealed record ContentType
{
    private static readonly HashSet<string> SupportedTypes =
    [
        "application/pdf",
        "application/msword",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        "application/vnd.ms-excel",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "text/plain",
        "image/png",
        "image/jpeg"
    ];

    public string Value { get; }

    private ContentType(string value)
    {
        Value = value;
    }

    public static ContentType Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Content type cannot be empty.",
                nameof(value));
        }

        var normalizedValue = value.Trim().ToLowerInvariant();

        if (!SupportedTypes.Contains(normalizedValue))
        {
            throw new ArgumentException(
                $"Content type '{value}' is not supported",
                nameof(value));
        }

        return new ContentType(normalizedValue);
    }

    public override string ToString()
    {
        return Value;
    }
}
