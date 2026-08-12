namespace DocumentManagementSystem.Domain.Documents.ValueObjects;

public sealed record StorageKey
{
    private const int MaxLength = 255;

    public string Value { get; }

    private StorageKey(string value)
    {
        Value = value;
    }

    public static StorageKey Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Storage key cannot be empty.",
                nameof(value));
        }

        var normalizedValue = value.Trim();

        if (normalizedValue.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Storage key cannot exceed {MaxLength} characters.",
                nameof(value));
        }

        if (IsAbsolutePath(normalizedValue))
        {
            throw new ArgumentException(
                "Storage key cannot be an absolute path.",
                nameof(value));
        }

        if (ContainsParentDirectorySegment(normalizedValue))
        {
            throw new ArgumentException(
                "Storage key cannot contain parent directory segments.",
                nameof(value));
        }

        return new StorageKey(normalizedValue);
    }

    public override string ToString()
    {
        return Value;
    }

    private static bool IsAbsolutePath(string value)
    {
        return value.StartsWith('/')
            || value.StartsWith('\\')
            || IsWindowsAbsolutePath(value);
    }

    private static bool IsWindowsAbsolutePath(string value)
    {
        return value.Length >= 3
            && char.IsLetter(value[0])
            && value[1] == ':'
            && (value[2] == '\\' || value[2] == '/');
    }

    private static bool ContainsParentDirectorySegment(string value)
    {
        var segments = value.Split(
            ['/', '\\'],
            StringSplitOptions.RemoveEmptyEntries);

        return segments.Any(segment =>
            string.Equals(
                segment,
                "..",
                StringComparison.Ordinal));
    }
}
