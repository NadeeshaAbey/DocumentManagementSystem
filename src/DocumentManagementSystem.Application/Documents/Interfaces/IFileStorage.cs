using DocumentManagementSystem.Domain.Documents.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Application.Documents.Interfaces;

public interface IFileStorage
{
    Task SaveAsync(StorageKey storageKey, Stream content, CancellationToken cancellationToken = default);
    Task<Stream> OpenReadAsync(StorageKey storageKey, CancellationToken cancellationToken = default);
    Task DeleteAsync(StorageKey storageKey, CancellationToken cancellationToken = default);

}
