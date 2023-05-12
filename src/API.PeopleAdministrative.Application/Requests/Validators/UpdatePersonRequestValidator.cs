using FluentValidation;

namespace API.PeopleAdministrative.Application.Requests.Validators;

public sealed class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
{
	public UpdatePersonRequestValidator()
	{
        RuleFor(p => p.Cpf).GreaterThan(0);
        RuleFor(p => p.Nome).NotEmpty();
        RuleFor(p => p.Endereco).NotEmpty();
    }
}
