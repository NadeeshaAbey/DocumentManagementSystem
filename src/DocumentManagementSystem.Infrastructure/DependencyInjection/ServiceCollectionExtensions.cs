using DocumentManagementSystem.Application.Documents.Interfaces;
using DocumentManagementSystem.Infrastructure.Persistence;
using DocumentManagementSystem.Infrastructure.Persistence.Repositories;
using DocumentManagementSystem.Infrastructure.Storage.Local;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string storageRoot)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IDocumentRepository, DocumentRepository>();

        services.AddSingleton<IFileStorage>(new LocalFileStorage(storageRoot));

        return services;
    }
}
