# Project Structure

## Version

- **Version:** 1.0
- **Status:** Draft

---

# Overview

The Document Management System follows the principles of **Clean Architecture**.

Each project has a single responsibility and dependencies always point toward the center of the architecture.

```text
Presentation (API)
        ↓
Application
        ↓
Domain

Infrastructure
        ↓
Application
        ↓
Domain
```

The Domain project has no dependencies on any other project.

---

# Solution Structure

```text
DocumentManagementSystem/

├── docs/
│
├── src/
│   ├── DocumentManagementSystem.Api/
│   ├── DocumentManagementSystem.Application/
│   ├── DocumentManagementSystem.Domain/
│   ├── DocumentManagementSystem.Infrastructure/
│   └── DocumentManagementSystem.Contracts/
│
├── tests/
│   ├── DocumentManagementSystem.Domain.Tests/
│   ├── DocumentManagementSystem.Application.Tests/
│   ├── DocumentManagementSystem.Infrastructure.Tests/
│   └── DocumentManagementSystem.Api.IntegrationTests/
│
├── storage/
│   └── documents/
│
├── .editorconfig
├── .gitignore
├── Directory.Build.props
├── Directory.Packages.props
├── DocumentManagementSystem.sln
└── README.md
```

---

# Project Responsibilities

## DocumentManagementSystem.Api

### Purpose

Exposes the REST API.

### Responsibilities

- Controllers
- Middleware
- Dependency Injection configuration
- Swagger/OpenAPI configuration
- Authentication (future)
- Authorization (future)
- Global exception handling
- Request validation integration

### Depends On

- Application
- Contracts

### Must NOT

- Contain business logic.
- Access the database directly.
- Access the file system directly.

---

## DocumentManagementSystem.Application

### Purpose

Implements application use cases.

### Responsibilities

- Application use cases
- Commands
- Queries
- Validators
- DTOs
- Mapping
- Interfaces
- Application services

### Depends On

- Domain
- Contracts

---

## DocumentManagementSystem.Domain

### Purpose

Contains the business model.

### Responsibilities

- Entities
- Value Objects
- Domain exceptions
- Repository abstractions
- Storage abstractions
- Business rules

### Depends On

Nothing.

The Domain project must remain independent of ASP.NET Core, Entity Framework Core, SQL Server, and file system implementations.

---

## DocumentManagementSystem.Infrastructure

### Purpose

Provides technical implementations.

### Responsibilities

- Entity Framework Core
- DbContext
- Repository implementations
- Local file storage
- Dependency Injection registrations
- Database migrations

### Depends On

- Domain
- Application

---

## DocumentManagementSystem.Contracts

### Purpose

Contains the API contract shared with API consumers.

### Responsibilities

- Request models
- Response models
- Error models

Contracts should remain stable even if internal implementation changes.

---

# Project Structure

## API

```text
DocumentManagementSystem.Api/

Controllers/
Middleware/
Extensions/
Configuration/
Filters/
```

---

## Application

The Application layer is organized **by feature** rather than by technical type.

```text
DocumentManagementSystem.Application/

Documents/
│
├── Commands/
│   ├── UploadDocument/
│   │   ├── UploadDocumentCommand.cs
│   │   ├── UploadDocumentCommandHandler.cs
│   │   ├── UploadDocumentValidator.cs
│   │   └── UploadDocumentMapping.cs
│   │
│   └── DeleteDocument/
│       ├── DeleteDocumentCommand.cs
│       ├── DeleteDocumentCommandHandler.cs
│       └── DeleteDocumentValidator.cs
│
├── Queries/
│   ├── GetDocument/
│   │   ├── GetDocumentQuery.cs
│   │   └── GetDocumentQueryHandler.cs
│   │
│   └── GetDocuments/
│       ├── GetDocumentsQuery.cs
│       └── GetDocumentsQueryHandler.cs
│
└── DTOs/
    └── DocumentDto.cs

Common/
├── Behaviors/
├── Exceptions/
├── Interfaces/
├── Mapping/
└── Services/
```

---

## Domain

```text
DocumentManagementSystem.Domain/

Entities/
│   └── Document.cs

ValueObjects/
│   ├── DocumentId.cs
│   ├── FileName.cs
│   ├── FileSize.cs
│   ├── ContentType.cs
│   └── StorageKey.cs

Repositories/
│   └── IDocumentRepository.cs

Storage/
│   └── IFileStorage.cs

Exceptions/
```

---

## Infrastructure

```text
DocumentManagementSystem.Infrastructure/

Persistence/
├── ApplicationDbContext.cs
├── Configurations/
├── Repositories/
└── Migrations/

Storage/
└── Local/
    └── LocalFileStorage.cs

DependencyInjection/
```

---

## Contracts

```text
DocumentManagementSystem.Contracts/

Requests/
├── UploadDocumentRequest.cs

Responses/
├── DocumentResponse.cs
├── DocumentSummaryResponse.cs

Errors/
└── ErrorResponse.cs
```

---

# Test Project Structure

```text
tests/

DocumentManagementSystem.Domain.Tests/

DocumentManagementSystem.Application.Tests/
└── Documents/
    ├── UploadDocument/
    ├── DeleteDocument/
    ├── GetDocument/
    └── GetDocuments/

DocumentManagementSystem.Infrastructure.Tests/

DocumentManagementSystem.Api.IntegrationTests/
```

---

# Dependency Rules

| Project | May Reference |
|----------|---------------|
| Api | Application, Contracts |
| Application | Domain, Contracts |
| Domain | None |
| Infrastructure | Domain, Application |
| Tests | Project under test |

Dependencies must always point inward.

---

# Configuration Files

## appsettings.json

Contains:

- Database connection string
- Storage root directory
- Maximum upload size
- Logging configuration

---

## Directory.Packages.props

Stores NuGet package versions in one place.

Benefits:

- Easier upgrades
- Consistent package versions
- Reduced duplication

---

## Directory.Build.props

Stores common MSBuild settings.

Examples:

- Target framework
- Nullable reference types
- Implicit usings
- Warning configuration
- Code analysis settings

---

## .editorconfig

Defines coding conventions for the entire solution.

---

# Local Storage Structure

Version 1 stores files on the local file system.

```text
storage/

└── documents/
    ├── 8e94b5d3.pdf
    ├── 91fef123.docx
    └── 73b66dc4.png
```

The generated file name is stored on disk.

The original file name is stored in the database.

The application stores only the `StorageKey`.

---

# Namespace Convention

Namespaces mirror the folder structure.

Examples:

```text
DocumentManagementSystem.Api.Controllers

DocumentManagementSystem.Application.Documents.Commands.UploadDocument

DocumentManagementSystem.Domain.Entities

DocumentManagementSystem.Infrastructure.Persistence.Repositories

DocumentManagementSystem.Contracts.Responses
```

---

# Design Principles

The solution follows:

- Clean Architecture
- Feature-Based Organization
- Separation of Concerns
- Dependency Inversion
- Single Responsibility Principle
- High Cohesion
- Low Coupling
- Testability
- Convention over Configuration

---

# Version 1 Scope

Version 1 includes:

- ASP.NET Core Web API
- SQL Server
- Entity Framework Core
- Local file storage
- Swagger/OpenAPI
- Unit tests
- Integration tests

Future versions may introduce additional projects for background processing, messaging, notifications, search, and client applications while preserving the existing architecture.
