using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace API.PeopleAdministrative.Shared.Responses;

public sealed record ApiError(string Message)
{
    [CompilerGenerated]
    private Type EqualityContract
    {
        [CompilerGenerated]
        get
        {
            return typeof(ApiError);
        }
    }

    [CompilerGenerated]
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ApiError");
        stringBuilder.Append(" { ");
        if (PrintMembers(stringBuilder))
        {
            stringBuilder.Append(' ');
        }

        stringBuilder.Append('}');
        return stringBuilder.ToString();
    }

    [CompilerGenerated]
    private bool PrintMembers(StringBuilder stringBuilder)
    {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        stringBuilder.Append("Message = ");
        stringBuilder.Append((object?)Message);
        return true;
    }

    [CompilerGenerated]
    public override int GetHashCode()
        => EqualityComparer<Type>
        .Default
        .GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);

    [CompilerGenerated]
    public bool Equals(ApiError? other)
        => (object)this == other 
        || ((object)other != null 
        && EqualityContract == other!.EqualityContract 
        && EqualityComparer<string>.Default.Equals(Message, other!.Message));

    [CompilerGenerated]
    public ApiError(ApiError original)
        => Message = original.Message;
}
