using System.ComponentModel.DataAnnotations;

namespace API.PeopleAdministrative.Shared.AppSettings;

public sealed class ConnectionStrings
{
    [Required]
    public string Database { get; private init; }
}
