using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Domain.Documents.ValueObjects;

public readonly record struct DocumentId(Guid Value)
{
    public static DocumentId New()
    {
        return new DocumentId(Guid.NewGuid());
    }

    public static DocumentId Empty()
    {
        return new DocumentId(Guid.Empty);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
