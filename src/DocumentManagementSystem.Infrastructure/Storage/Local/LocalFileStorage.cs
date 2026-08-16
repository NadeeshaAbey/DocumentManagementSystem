using DocumentManagementSystem.Application.Documents.Interfaces;
using DocumentManagementSystem.Domain.Documents.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Infrastructure.Storage.Local;

public sealed class LocalFileStorage : IFileStorage
{
    private readonly string _storageRoot;

    public LocalFileStorage(string storageRoot)
    {
        if (string.IsNullOrWhiteSpace(storageRoot))
        {
            throw new ArgumentException(
                "Storage root cannot be empty.",
                nameof(storageRoot));
        }

        _storageRoot = Path.GetFullPath(storageRoot);

        Directory.CreateDirectory(_storageRoot);
    }

    public async Task SaveAsync(StorageKey storageKey, Stream content, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);

        var filePath = GetSafeFilePath(storageKey);

        var directory = Path.GetDirectoryName(filePath);

        if(directory is not null)
        {
            Directory.CreateDirectory(directory);
        }

        await using var fileStream = new FileStream(
            filePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 81920,
            useAsync: true);

        await content.CopyToAsync(fileStream, cancellationToken);
    }

    public Task<Stream> OpenReadAsync(StorageKey storageKey, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filePath = GetSafeFilePath(storageKey);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                "The requested document was not found.",
                filePath);
        }

        Stream stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 81920,
            useAsync: true);

        return Task.FromResult(stream);
    }

    public Task DeleteAsync(
        StorageKey storageKey,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filePath = GetSafeFilePath(storageKey);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }

    private string GetSafeFilePath(StorageKey storageKey)
    {
        var combinedPath = Path.Combine(
            _storageRoot,
            storageKey.Value);

        var fullPath = Path.GetFullPath(combinedPath);

        var rootWithSeparator =
            _storageRoot.EndsWith(
                Path.DirectorySeparatorChar)
                ? _storageRoot
                : _storageRoot + Path.DirectorySeparatorChar;

        if (!fullPath.StartsWith(
                rootWithSeparator,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Storage key resolves outside the configured storage directory.");
        }

        return fullPath;
    }
}
