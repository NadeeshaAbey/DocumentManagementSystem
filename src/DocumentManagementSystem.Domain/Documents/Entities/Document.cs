using DocumentManagementSystem.Domain.Documents.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Domain.Documents.Entities;

public sealed class Document
{
    public DocumentId Id { get; }
    public FileName FileName { get; }
    public ContentType ContentType { get; }
    public FileSize FileSize { get; }
    public StorageKey StorageKey { get; }
    public DateTimeOffset UploadedAt { get; }

    private Document(DocumentId id, FileName fileName, ContentType contentType, FileSize fileSize, StorageKey storageKey, DateTimeOffset uploadedAt)
    {
        Id = id;
        FileName = fileName;
        ContentType = contentType;
        FileSize = fileSize;
        StorageKey = storageKey;
        UploadedAt = uploadedAt;
    }

    public static Document Create(
        FileName fileName,
        ContentType contentType,
        FileSize fileSize,
        StorageKey storageKey,
        DateTimeOffset uploadedAt)
    {
        var id = DocumentId.New();

        return new Document(
            id,
            fileName,
            contentType,
            fileSize,
            storageKey,
            uploadedAt);
    }
}
