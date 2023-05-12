using FluentValidation.Results;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Shared.Messages;

public abstract class BaseRequestWithValidation : BaseRequest
{
    protected BaseRequestWithValidation()
    {
        ValidationResult = new ValidationResult();
    }

    [JsonIgnore]
    public ValidationResult ValidationResult { get; protected set; }

    /// <summary>
    /// Indica se a requisição é válida.
    /// </summary>
    [JsonIgnore]
    public bool IsValid => ValidationResult.IsValid;

    /// <summary>
    /// Valida a requisição.
    /// </summary>
    public abstract Task ValidateAsync();
}
