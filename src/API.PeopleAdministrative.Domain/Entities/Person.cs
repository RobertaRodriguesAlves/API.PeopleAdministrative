using API.PeopleAdministrative.Shared.Abstractions;

namespace API.PeopleAdministrative.Domain.Entities;

public sealed class Person : IEntity
{
    public long Cpf { get; private set; }
    public string Nome { get; private set; }
    public string Endereco { get; private set; }
    public bool Ativo { get; private set; }

    public Person SetCpf(long cpf)
    {
        Cpf = cpf;
        return this;
    }

    public Person SetNome(string nome)
    {
        Nome = nome;
        return this;
    }

    public Person SetEndereco(string endereco)
    {
        Endereco = endereco;
        return this;
    }

    public Person SetAtivo(bool ativo)
    {
        Ativo = ativo;
        return this;
    }
}
