using DocumentManagementSystem.Application.Documents.Interfaces;
using DocumentManagementSystem.Domain.Documents.Entities;
using DocumentManagementSystem.Domain.Documents.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class DocumentRepository(ApplicationDbContext dbContext) : IDocumentRepository
{
    public async Task AddAsync(Document document, CancellationToken cancellationToken = default)
    {
        await dbContext.Documents.AddAsync(
            document,
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Document document, CancellationToken cancellationToken = default)
    {
        dbContext.Documents.Remove(document);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Documents
            .AsNoTracking()
            .OrderByDescending(document => document.UploadedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Document?> GetByIdAsync(DocumentId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Documents
            .SingleOrDefaultAsync(
                document => document.Id == id,
                cancellationToken);
    }
}
