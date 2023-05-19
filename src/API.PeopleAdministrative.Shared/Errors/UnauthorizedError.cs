using FluentResults;
using System.Diagnostics.CodeAnalysis;

namespace API.PeopleAdministrative.Shared.Errors;

[ExcludeFromCodeCoverage]
public sealed class UnauthorizedError : Error
{
	public UnauthorizedError()
	{

	}

	public UnauthorizedError(string message)
		: base(message)
	{

	}

	public UnauthorizedError(string message, Error causedBy)
		: base(message, causedBy)
	{

	}
}
