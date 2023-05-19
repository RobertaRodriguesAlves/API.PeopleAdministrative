using FluentResults;
using System.Diagnostics.CodeAnalysis;

namespace API.PeopleAdministrative.Shared.Errors;

[ExcludeFromCodeCoverage]
public sealed class ForbiddenError : Error
{
	public ForbiddenError()
	{

	}

	public ForbiddenError(string message)
		: base(message)
	{

	}

	public ForbiddenError(string message, Error causedBy)
		: base(message, causedBy)
	{

	}
}
