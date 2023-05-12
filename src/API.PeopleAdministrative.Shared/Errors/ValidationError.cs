using FluentResults;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace API.PeopleAdministrative.Shared.Errors;

[ExcludeFromCodeCoverage]
public sealed class ValidationError : Error
{
    public ValidationError(string message)
        : base(message) { }

    public ValidationError(ValidationFailure failure)
        : base(failure.ErrorMessage) { }
}
