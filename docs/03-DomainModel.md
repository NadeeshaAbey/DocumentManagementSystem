# Domain Model

## Overview

The Document Management System domain is centered around managing documents and their associated metadata. In Version 1, a document consists of two parts:

1. The physical file.
2. The metadata describing the file.

The domain does not concern itself with *how* files are stored (local disk, Azure Blob Storage, etc.). It only defines the business concepts and rules.

---

# Ubiquitous Language

| Term | Definition |
|------|------------|
| Document | A file uploaded by a user together with its metadata. |
| Metadata | Information describing a document, such as its name, size, and upload date. |
| File | The binary content of the uploaded document. |
| Storage Location | A reference indicating where the physical file is stored. |

---

# Entities

## Document

The `Document` is the primary entity in the system.

### Responsibilities

- Represents an uploaded document.
- Maintains document metadata.
- Knows where the file is stored.
- Enforces domain rules related to a document.

### Properties

| Property | Description |
|-----------|-------------|
| Id | Unique identifier |
| FileName | Original file name |
| ContentType | MIME type |
| FileSize | Size in bytes |
| StorageKey | Unique identifier used by the storage provider to locate the file |
| UploadedAt | Date and time uploaded |
| LastModifiedAt | Date and time last modified |

---

# Value Objects

## DocumentId

Represents the unique identity of a document.

Characteristics:

- Immutable
- Unique
- Never changes

---

## FileName

Represents the original file name.

Rules:

- Cannot be empty.
- Must contain a valid file name.
- Includes the extension.

Examples:

- `invoice.pdf`
- `contract.docx`

---

## ContentType

Represents the MIME type.

Examples:

- application/pdf
- application/vnd.openxmlformats-officedocument.wordprocessingml.document
- image/png
- image/jpeg

---

## FileSize

Represents the size of the file.

Rules:

- Greater than zero.
- Cannot exceed the configured maximum upload size.

---

## StorageKey

Represents the unique identifier used by the storage provider to locate the physical file.

Examples:

- documents/8e94b5d3.pdf
- documents/2026/07/contract.pdf

The domain does not interpret this value. Only the storage provider knows how to resolve it.

---

# Aggregate

## Document Aggregate

The `Document` aggregate is responsible for maintaining the consistency of document-related data.

For Version 1, the aggregate contains only the `Document` entity.

---

# Domain Invariants

The following rules must always be true:

- Every document has a unique identifier.
- Every document has a file name.
- Every document has a valid content type.
- Every document has a positive file size.
- Every document has a valid storage key.
- Every document has an upload timestamp.
- A document cannot exist without its metadata.

---

# Domain Operations

The domain supports the following operations:

- Create a document.
- Retrieve a document.
- List documents.
- Delete a document.

Updating document metadata is outside the scope of Version 1.

---

# Relationships

Current relationships:

```
Document
```

Future relationships:

```
User
 └── Documents

Folder
 └── Documents

Category
 └── Documents

Tag
 └── Documents

Document
 └── Versions

Document
 └── Audit Logs
```

These relationships are intentionally deferred to later versions.

---

# Future Domain Expansion

Future versions may introduce additional entities:

- User
- Folder
- Category
- Tag
- DocumentVersion
- AuditLog
- Comment
- Permission
- ShareLink
- ApprovalWorkflow

The current domain model is intentionally minimal to keep Version 1 focused on the core document management workflow.

---

# Domain Boundaries

The domain is responsible for:

- Document rules
- Metadata validation
- Business invariants

The domain is **not** responsible for:

- File storage implementation
- Database access
- HTTP APIs
- Authentication
- Authorization
- Logging
- Caching
- Cloud services

These concerns belong to other layers of the system.
