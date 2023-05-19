using FluentResults;
using System.Diagnostics.CodeAnalysis;

namespace API.PeopleAdministrative.Shared.Errors;

[ExcludeFromCodeCoverage]
public sealed class NotFoundError : Error
{
    public NotFoundError()
    {

    }

    public NotFoundError(string message)
        : base(message)
    {

    }

    public NotFoundError(string message, Error causedBy)
        : base(message, causedBy)
    {

    }
}