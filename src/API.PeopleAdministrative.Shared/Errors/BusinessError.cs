using FluentResults;
using System.Diagnostics.CodeAnalysis;

namespace API.PeopleAdministrative.Shared.Errors;

[ExcludeFromCodeCoverage]
public sealed class BusinessError : Error
{
    public BusinessError()
    {

    }

    public BusinessError(string message)
        : base(message) { }

    public BusinessError(string message, Error causedBy)
        : base(message, causedBy) { }
}