# Coding Standards

## Version

- **Version:** 1.0
- **Status:** Approved

---

# Purpose

This document defines the coding standards for the Document Management System to ensure consistency, readability, and maintainability.

---

# General Principles

- Write code for humans first.
- Prefer clarity over cleverness.
- Keep methods small and focused.
- Follow SOLID principles.
- Avoid duplication (DRY).
- Keep classes cohesive.

---

# Naming Conventions

## Projects

```
DocumentManagementSystem.Api
DocumentManagementSystem.Application
DocumentManagementSystem.Domain
DocumentManagementSystem.Infrastructure
DocumentManagementSystem.Contracts
```

## Classes

Use PascalCase.

```
DocumentService
UploadDocumentCommand
DocumentRepository
```

## Interfaces

Prefix with `I`.

```
IDocumentRepository
IFileStorage
```

## Methods

Use verbs.

```
UploadAsync()
DeleteAsync()
GetByIdAsync()
```

## Variables

Use camelCase.

```
document
storageKey
uploadedFile
```

## Constants

Use PascalCase.

```
MaxUploadSize
AllowedExtensions
```

---

# Async Guidelines

- Use `async`/`await` end-to-end.
- Suffix asynchronous methods with `Async`.
- Avoid `.Result` and `.Wait()`.
- Pass `CancellationToken` to I/O operations.

Example:

```csharp
Task<Document> GetByIdAsync(Guid id, CancellationToken cancellationToken);
```

---

# Exception Handling

- Throw exceptions only for exceptional situations.
- Do not swallow exceptions.
- Use global exception handling in the API.
- Include meaningful exception messages.

---

# Validation

- Validate requests using FluentValidation.
- Keep validation outside controllers.
- Keep domain invariants inside domain entities.

---

# Logging

Log meaningful events:

- Upload started
- Upload completed
- Download requested
- Delete completed
- Unexpected failures

Do not log:

- Passwords
- Secrets
- Connection strings
- Sensitive document contents

---

# Dependency Injection

- Depend on abstractions.
- Constructor injection only.
- Avoid service locators.
- Keep constructors reasonably small.

---

# Entity Framework Core

- Use asynchronous methods.
- Configure entities with Fluent API.
- Avoid lazy loading.
- Use `AsNoTracking()` for read-only queries.

---

# API Controllers

Controllers should:

- Validate the request.
- Delegate work to the Application layer.
- Return HTTP responses.

Controllers should not:

- Contain business logic.
- Access DbContext directly.
- Access file storage directly.

---

# Comments

Prefer self-explanatory code.

Add comments only when explaining intent or non-obvious decisions.

---

# Testing

- Write unit tests for business logic.
- Use descriptive test names.

Example:

```
UploadDocument_ShouldReturnCreated_WhenRequestIsValid
```

---

# Formatting

- Four-space indentation.
- One class per file.
- One public type per file.
- File name matches the public type.
- Enable nullable reference types.
- Enable implicit usings.

---

# Code Reviews

Every pull request should verify:

- Correctness
- Readability
- Test coverage
- Performance considerations
- Security considerations
- Maintainability
