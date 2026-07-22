# Technology Decisions

## Version

- **Version:** 1.0
- **Status:** Approved

---

# Overview

This document records the major architectural and technology decisions made for Version 1 of the Document Management System.

Each decision includes the selected technology, the rationale, alternatives considered, and the expected impact.

---

# ADR-001: Architecture Style

## Decision

Use **Clean Architecture**.

## Rationale

- Separates business logic from infrastructure.
- Encourages maintainable, testable code.
- Makes infrastructure replaceable.
- Supports long-term scalability.

## Alternatives Considered

- Layered Architecture
- Vertical Slice Architecture
- N-Tier Architecture

## Consequences

### Positive

- Clear separation of concerns.
- Easier unit testing.
- Reduced coupling.
- Flexible infrastructure.

### Negative

- More projects.
- Slightly higher learning curve.
- More initial setup.

---

# ADR-002: Backend Framework

## Decision

Use **ASP.NET Core Web API**.

## Rationale

- Modern, high-performance framework.
- Cross-platform.
- First-class OpenAPI support.
- Excellent dependency injection.
- Large ecosystem.

## Alternatives Considered

- Minimal APIs
- gRPC
- Node.js (Express/NestJS)

## Consequences

### Positive

- Industry standard.
- Easy integration with EF Core.
- Mature middleware pipeline.

### Negative

- More boilerplate than Minimal APIs.

---

# ADR-003: Programming Language

## Decision

Use **C#**.

## Rationale

- Primary development language.
- Strong typing.
- Excellent tooling.
- Rich ecosystem.

## Alternatives Considered

None.

---

# ADR-004: Database

## Decision

Use **SQL Server** with **Entity Framework Core**.

## Rationale

- Reliable relational database.
- Widely used in enterprise .NET applications.
- Excellent EF Core support.
- Easy migration path to Azure SQL.

## Alternatives Considered

- SQLite
- PostgreSQL
- MySQL

## Consequences

### Positive

- Strong tooling.
- Supports future growth.
- Familiar SQL capabilities.

### Negative

- Requires a SQL Server instance during development.

---

# ADR-005: ORM

## Decision

Use **Entity Framework Core**.

## Rationale

- Reduces boilerplate.
- Strong LINQ support.
- Database migrations.
- Good balance between productivity and control.

## Alternatives Considered

- Dapper
- Raw ADO.NET

## Consequences

### Positive

- Faster development.
- Easier maintenance.
- Strong community support.

### Negative

- Can generate inefficient queries if used incorrectly.

---

# ADR-006: File Storage

## Decision

Use **Local File Storage** for Version 1.

## Rationale

- Simple to implement.
- Easy to debug.
- No cloud dependencies.
- Keeps focus on architecture.

The application stores a **StorageKey**, not a physical file path.

## Alternatives Considered

- Azure Blob Storage
- Amazon S3
- Database BLOB storage

## Consequences

### Positive

- Simple local development.
- Easy replacement with cloud storage.
- Faster implementation.

### Negative

- Not suitable for distributed deployments.

---

# ADR-007: Testing Framework

## Decision

Use:

- xUnit
- FluentAssertions
- Moq

## Rationale

These libraries are widely adopted in the .NET ecosystem and work well together.

### Responsibilities

| Library | Purpose |
|----------|---------|
| xUnit | Test framework |
| FluentAssertions | Readable assertions |
| Moq | Mocking dependencies |

---

# ADR-008: API Documentation

## Decision

Use **Swagger / OpenAPI**.

## Rationale

- Interactive documentation.
- Easy endpoint testing.
- Automatic API specification generation.

## Alternatives Considered

- Postman collections only
- Manual documentation

---

# ADR-009: Dependency Injection

## Decision

Use the built-in ASP.NET Core Dependency Injection container.

## Rationale

- Built into the framework.
- Sufficient for project requirements.
- Reduces external dependencies.

## Alternatives Considered

- Autofac
- Lamar

---

# ADR-010: Validation

## Decision

Use **FluentValidation**.

## Rationale

- Keeps validation outside controllers.
- Easy to test.
- Expressive rule definitions.

## Alternatives Considered

- Data Annotations
- Custom validation logic

---

# ADR-011: Logging

## Decision

Use **Microsoft.Extensions.Logging**.

## Rationale

- Built into ASP.NET Core.
- Provider-based architecture.
- Easily extended with Serilog or other providers later.

---

# ADR-012: API Style

## Decision

Use **REST**.

## Rationale

- Industry standard.
- Stateless communication.
- Broad client compatibility.
- Well-understood conventions.

## Alternatives Considered

- GraphQL
- gRPC

---

# ADR-013: Repository Pattern

## Decision

Use repository abstractions in the Domain layer with implementations in the Infrastructure layer.

## Rationale

- Decouples business logic from persistence.
- Improves testability.
- Supports changing persistence technologies.

## Alternatives Considered

- Direct `DbContext` usage in the Application layer.

---

# ADR-014: File Storage Abstraction

## Decision

Use an `IFileStorage` interface.

## Rationale

The Application layer should depend on an abstraction rather than a specific storage implementation.

Version 1:

```text
Application
        ↓
IFileStorage
        ↓
LocalFileStorage
```

Future versions:

```text
Application
        ↓
IFileStorage
        ↓
AzureBlobFileStorage
```

---

# ADR-015: Solution Organization

## Decision

Organize the Application layer by **feature**.

Example:

```text
Documents/
├── Commands/
├── Queries/
├── DTOs/
└── Validators/
```

## Rationale

- Easier navigation.
- Better scalability.
- Related code stays together.

## Alternatives Considered

- Layer-first organization (`Commands/`, `Queries/`, `DTOs/` at the root).

---

# Summary

| Area | Decision |
|------|----------|
| Architecture | Clean Architecture |
| Backend | ASP.NET Core Web API |
| Language | C# |
| Database | SQL Server |
| ORM | Entity Framework Core |
| File Storage | Local File Storage |
| Storage Abstraction | `IFileStorage` |
| API Style | REST |
| Testing | xUnit + FluentAssertions + Moq |
| Validation | FluentValidation |
| Logging | Microsoft.Extensions.Logging |
| API Documentation | Swagger / OpenAPI |
| Dependency Injection | Built-in ASP.NET Core DI |
| Application Structure | Feature-Based Organization |

---

# Future Revisions

Version 2 may introduce:

- Azure Blob Storage
- Azure SQL Database
- Authentication & Authorization
- Background processing
- Full-text search
- Document versioning
- Audit logging

These enhancements should build on the existing architecture without requiring changes to the Domain layer.
