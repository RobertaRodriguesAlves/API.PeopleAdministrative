using API.PeopleAdministrative.Application.Requests.Validators;
using API.PeopleAdministrative.Shared.Messages;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Application.Requests;

public class CreatePersonRequest : BaseRequestWithValidation
{
    public CreatePersonRequest(int cpf, string nome, string endereco)
    {
        Cpf = cpf;
        Nome = nome;
        Endereco = endereco;
    }

    public int Cpf { get; private set; }
    public string Nome { get; private set; }
    public string Endereco { get; private set; }

    public override async Task ValidateAsync()
        => ValidationResult = await new CreatePersonRequestValidator().ValidateAsync(this);
}
