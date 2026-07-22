# Testing Strategy

## Version

- **Version:** 1.0
- **Status:** Approved

---

# Goal

Ensure the system is reliable, maintainable, and safe to refactor through automated testing.

---

# Testing Pyramid

```
        Integration Tests
             ▲
             │
        Unit Tests
```

Version 1 focuses primarily on unit tests, with a smaller set of integration tests.

---

# Test Framework

| Tool | Purpose |
|------|---------|
| xUnit | Test framework |
| FluentAssertions | Assertions |
| Moq | Mocking |
| ASP.NET Core Test Host | Integration testing |

---

# Unit Testing

Unit tests should verify:

- Domain entities
- Value objects
- Application services
- Command handlers
- Query handlers
- Validators

They should not access:

- SQL Server
- File system
- External services

---

# Integration Testing

Integration tests verify:

- API endpoints
- Entity Framework Core configuration
- Dependency injection
- Database interactions
- File storage interactions

---

# Test Organization

```
tests/

Domain.Tests/
Application.Tests/
Infrastructure.Tests/
Api.IntegrationTests/
```

Within each project, organize tests by feature.

---

# Naming Convention

```
Method_ShouldExpectedBehavior_WhenCondition
```

Examples:

```
UploadDocument_ShouldCreateDocument_WhenRequestIsValid

DeleteDocument_ShouldReturnNotFound_WhenDocumentDoesNotExist
```

---

# Mocking

Mock only external dependencies such as:

- Repositories
- File storage
- Time providers

Do not mock value objects or domain entities.

---

# Test Data

Keep test data:

- Minimal
- Readable
- Focused on the scenario

Avoid large shared datasets.

---

# Coverage Goals

| Layer | Target |
|--------|-------:|
| Domain | 95%+ |
| Application | 90%+ |
| Infrastructure | Meaningful integration coverage |
| API | Endpoint integration coverage |

Coverage is a guide, not the objective. Test important behavior rather than chasing percentages.

---

# When to Write Tests

Follow this workflow:

1. Define requirements.
2. Design the solution.
3. Implement the feature.
4. Write or complete unit tests.
5. Refactor.
6. Run all tests.
7. Perform code review.

As confidence grows, consider adopting Test-Driven Development for suitable features.

---

# Continuous Validation

Before every commit:

- Build succeeds.
- All tests pass.
- No compiler warnings introduced.
- Code formatting applied.
