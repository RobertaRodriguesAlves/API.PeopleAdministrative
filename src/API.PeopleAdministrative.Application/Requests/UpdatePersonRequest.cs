using API.PeopleAdministrative.Application.Requests.Validators;
using API.PeopleAdministrative.Shared.Messages;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Application.Requests;

public sealed class UpdatePersonRequest : BaseRequestWithValidation
{
    public UpdatePersonRequest(long cpf, string nome, string endereco)
    {
        Cpf = cpf;
        Nome = nome;
        Endereco = endereco;
    }

    public long Cpf { get; private set; }
    public string Nome { get; private set; }
    public string Endereco { get; private set; }

    public override async Task ValidateAsync()
        => ValidationResult = await new UpdatePersonRequestValidator().ValidateAsync(this);
}
