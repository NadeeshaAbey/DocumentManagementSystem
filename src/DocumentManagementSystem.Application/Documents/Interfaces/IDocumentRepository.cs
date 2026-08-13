using DocumentManagementSystem.Domain.Documents.Entities;
using DocumentManagementSystem.Domain.Documents.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Application.Documents.Interfaces;

public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(DocumentId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Document document, CancellationToken cancellationToken = default);
    Task DeleteAsync(Document document, CancellationToken cancellationToken = default);
}
