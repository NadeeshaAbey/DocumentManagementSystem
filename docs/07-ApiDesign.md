# API Design

## Version

- **Version:** 1.0
- **Status:** Draft

---

# Overview

The Document Management System exposes a RESTful HTTP API for managing documents.

The API allows clients to:

- Upload documents
- List documents
- Retrieve document metadata
- Download documents
- Delete documents

The API is designed to be:

- RESTful
- Stateless
- Consistent
- Versionable
- Easy to consume
- Easy to extend

---

# Base URL

```
/api/v1
```

---

# Content Types

## Request Content Types

```
application/json
multipart/form-data
```

## Response Content Types

```
application/json
application/octet-stream
```

---

# Resource

The primary resource is:

```
Document
```

---

# Resource Representation

A document resource is represented as:

```json
{
  "id": "73b66dc4-fd44-4d52-b76d-1d4fc4d529d1",
  "originalFileName": "invoice.pdf",
  "contentType": "application/pdf",
  "fileSize": 245761,
  "uploadedAt": "2026-07-18T09:45:12Z",
  "_links": {
    "self": "/api/v1/documents/73b66dc4-fd44-4d52-b76d-1d4fc4d529d1",
    "download": "/api/v1/documents/73b66dc4-fd44-4d52-b76d-1d4fc4d529d1/download"
  }
}
```

---

# API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/v1/documents | Upload a document |
| GET | /api/v1/documents | Retrieve all documents |
| GET | /api/v1/documents/{id} | Retrieve document metadata |
| GET | /api/v1/documents/{id}/download | Download a document |
| DELETE | /api/v1/documents/{id} | Delete a document |

---

# Upload Document

## Endpoint

```
POST /api/v1/documents
```

## Purpose

Uploads a new document.

---

## Request

### Content-Type

```
multipart/form-data
```

### Request Body

| Field | Type | Required | Description |
|------|------|----------|-------------|
| file | File | Yes | Document to upload |

---

## Success Response

```
201 Created
```

### Headers

```
Location: /api/v1/documents/{id}
```

Example

```
Location: /api/v1/documents/73b66dc4-fd44-4d52-b76d-1d4fc4d529d1
```

### Response Body

```json
{
  "id": "73b66dc4-fd44-4d52-b76d-1d4fc4d529d1",
  "originalFileName": "invoice.pdf",
  "contentType": "application/pdf",
  "fileSize": 245761,
  "uploadedAt": "2026-07-18T09:45:12Z",
  "_links": {
    "self": "/api/v1/documents/73b66dc4-fd44-4d52-b76d-1d4fc4d529d1",
    "download": "/api/v1/documents/73b66dc4-fd44-4d52-b76d-1d4fc4d529d1/download"
  }
}
```

---

## Error Responses

### Bad Request

```
400 Bad Request
```

Examples:

- Missing file
- Empty file
- File exceeds maximum size

---

### Unsupported Media Type

```
415 Unsupported Media Type
```

---

### Internal Server Error

```
500 Internal Server Error
```

---

# List Documents

## Endpoint

```
GET /api/v1/documents
```

## Purpose

Returns metadata for all documents.

---

## Success Response

```
200 OK
```

```json
[
  {
    "id": "73b66dc4-fd44-4d52-b76d-1d4fc4d529d1",
    "originalFileName": "invoice.pdf",
    "contentType": "application/pdf",
    "fileSize": 245761,
    "uploadedAt": "2026-07-18T09:45:12Z"
  }
]
```

---

# Get Document Details

## Endpoint

```
GET /api/v1/documents/{id}
```

## Purpose

Returns metadata for a single document.

---

## Success Response

```
200 OK
```

```json
{
  "id": "73b66dc4-fd44-4d52-b76d-1d4fc4d529d1",
  "originalFileName": "invoice.pdf",
  "contentType": "application/pdf",
  "fileSize": 245761,
  "uploadedAt": "2026-07-18T09:45:12Z",
  "_links": {
    "self": "/api/v1/documents/73b66dc4-fd44-4d52-b76d-1d4fc4d529d1",
    "download": "/api/v1/documents/73b66dc4-fd44-4d52-b76d-1d4fc4d529d1/download"
  }
}
```

---

## Error Response

```
404 Not Found
```

---

# Download Document

## Endpoint

```
GET /api/v1/documents/{id}/download
```

## Purpose

Downloads the original document.

---

## Success Response

```
200 OK
```

Headers

```
Content-Type: application/pdf

Content-Disposition: attachment; filename="invoice.pdf"
```

Response Body

```
Binary file
```

---

## Error Response

```
404 Not Found
```

---

# Delete Document

## Endpoint

```
DELETE /api/v1/documents/{id}
```

## Purpose

Deletes a document and its associated file.

---

## Success Response

```
204 No Content
```

---

## Error Response

```
404 Not Found
```

---

# Validation Rules

## File

- File is required.
- File size must be greater than zero.
- File size must not exceed the configured maximum upload size.

---

## Supported File Types

Version 1 supports:

- PDF
- DOC
- DOCX
- XLS
- XLSX
- TXT
- PNG
- JPG
- JPEG

---

# HTTP Status Codes

| Status | Description |
|---------|-------------|
| 200 OK | Request completed successfully |
| 201 Created | Document created successfully |
| 204 No Content | Document deleted successfully |
| 400 Bad Request | Invalid request |
| 404 Not Found | Document not found |
| 415 Unsupported Media Type | Unsupported file type |
| 500 Internal Server Error | Unexpected server error |

---

# Standard Error Response

All API errors use a consistent response format.

```json
{
  "type": "ValidationError",
  "title": "Invalid request",
  "status": 400,
  "detail": "Uploaded file exceeds the maximum allowed size."
}
```

---

# API Design Principles

The API follows these principles:

- Resource-oriented URLs
- Correct use of HTTP methods
- Stateless communication
- Appropriate HTTP status codes
- Consistent request and response formats
- Predictable error handling
- Self-descriptive resource representations
- Created resources include a `Location` header
- Clients interact with resources rather than implementation details

---

# Version 1 Scope

Version 1 intentionally excludes:

- Authentication
- Authorization
- Pagination
- Sorting
- Filtering
- Search
- Bulk upload
- Bulk delete
- Document versioning
- Rate limiting

These capabilities will be introduced in future versions.

---

# Future Enhancements

Future versions may add:

- Pagination
- Sorting
- Filtering
- Full-text search
- Authentication
- Authorization
- API rate limiting
- Bulk operations
- Document versioning
- ETags and optimistic concurrency
- Cursor-based pagination
- OpenAPI client generation
