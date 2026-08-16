using DocumentManagementSystem.Domain.Documents.ValueObjects;
using DocumentManagementSystem.Infrastructure.Storage.Local;
using FluentAssertions;

namespace DocumentManagementSystem.Infrastructure.Tests.Storage.Local;

public sealed class LocalFileStorageTests :IDisposable
{
    private readonly string _storageRoot;
    private readonly LocalFileStorage _storage;

    public LocalFileStorageTests()
    {
        _storageRoot = Path.Combine(
            Path.GetTempPath(),
            "DocumentManagementSystemTests",
            Guid.NewGuid().ToString());

        _storage = new LocalFileStorage(_storageRoot);
    }

    [Fact]
    public async Task SaveAsync_ShouldCreateFile()
    {
        var storageKey = StorageKey.Create("documents/test.txt");
        await using var content = CreateStream("Hello World");

        await _storage.SaveAsync(storageKey, content);

        var filePath = Path.Combine(
            _storageRoot,
            "documents",
            "test.txt");

        File.Exists(filePath).Should().BeTrue();
    }

    [Fact]
    public async Task SaveAsync_ShouldPreserveFileContent()
    {
        var storageKey = StorageKey.Create("documents/test.txt");
        await using var content = CreateStream("Hello World");

        await _storage.SaveAsync(storageKey, content);

        await using var storedContent =
            await _storage.OpenReadAsync(storageKey);

        using var reader = new StreamReader(storedContent);

        var result = await reader.ReadToEndAsync();

        result.Should().Be("Hello World");
    }

    [Fact]
    public async Task SaveAsync_ShouldCreateNestedDirectories()
    {
        var storageKey = StorageKey.Create(
            "documents/2026/08/report.txt");

        await using var content = CreateStream("Report");

        await _storage.SaveAsync(storageKey, content);

        var filePath = Path.Combine(
            _storageRoot,
            "documents",
            "2026",
            "08",
            "report.txt");

        File.Exists(filePath).Should().BeTrue();
    }

    [Fact]
    public async Task SaveAsync_ShouldThrow_WhenFileAlreadyExists()
    {
        var storageKey = StorageKey.Create("documents/test.txt");

        await using (var firstContent = CreateStream("First"))
        {
            await _storage.SaveAsync(storageKey, firstContent);
        }

        await using var secondContent = CreateStream("Second");

        var action = () =>
            _storage.SaveAsync(storageKey, secondContent);

        await action.Should()
            .ThrowAsync<IOException>();
    }

    [Fact]
    public async Task OpenReadAsync_ShouldThrow_WhenFileDoesNotExist()
    {
        var storageKey = StorageKey.Create(
            "documents/missing.txt");

        var action = () =>
            _storage.OpenReadAsync(storageKey);

        await action.Should()
            .ThrowAsync<FileNotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteExistingFile()
    {
        var storageKey = StorageKey.Create("documents/test.txt");

        await using (var content = CreateStream("Hello"))
        {
            await _storage.SaveAsync(storageKey, content);
        }

        await _storage.DeleteAsync(storageKey);

        var filePath = Path.Combine(
            _storageRoot,
            "documents",
            "test.txt");

        File.Exists(filePath).Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotThrow_WhenFileDoesNotExist()
    {
        var storageKey = StorageKey.Create(
            "documents/missing.txt");

        var action = () =>
            _storage.DeleteAsync(storageKey);

        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async Task SaveAsync_ShouldRespectCancellation()
    {
        var storageKey = StorageKey.Create("documents/test.txt");
        await using var content = CreateStream("Hello");

        using var cancellationTokenSource =
            new CancellationTokenSource();

        await cancellationTokenSource.CancelAsync();

        var action = () =>
            _storage.SaveAsync(
                storageKey,
                content,
                cancellationTokenSource.Token);

        await action.Should()
            .ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OpenReadAsync_ShouldRespectCancellation()
    {
        var storageKey = StorageKey.Create("documents/test.txt");

        await using (var content = CreateStream("Hello"))
        {
            await _storage.SaveAsync(storageKey, content);
        }

        using var cancellationTokenSource =
            new CancellationTokenSource();

        await cancellationTokenSource.CancelAsync();

        var action = () =>
            _storage.OpenReadAsync(
                storageKey,
                cancellationTokenSource.Token);

        await action.Should()
            .ThrowAsync<OperationCanceledException>();
    }

    private static MemoryStream CreateStream(string content)
    {
        return new MemoryStream(
            System.Text.Encoding.UTF8.GetBytes(content));
    }

    public void Dispose()
    {
        if (Directory.Exists(_storageRoot))
        {
            Directory.Delete(
                _storageRoot,
                recursive: true);
        }
    }
}
