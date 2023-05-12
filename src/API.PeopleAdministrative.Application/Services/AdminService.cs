using API.PeopleAdministrative.Application.Interfaces;
using API.PeopleAdministrative.Application.Requests;
using API.PeopleAdministrative.Domain.Entities;
using API.PeopleAdministrative.Domain.Interfaces;
using API.PeopleAdministrative.Shared.Extensions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Application.Services;

public sealed class AdminService : IAdminService
{
    private readonly IPeopleRepository _repository;

    public AdminService(IPeopleRepository repository)
        =>_repository = repository;

    public async Task<Result> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default)
    {
        await request.ValidateAsync();
        if (!request.IsValid)
            return request.ToFail();

        var person = new Person();
        person.SetCpf(request.Cpf);
        person.SetNome(request.Nome);
        person.SetEndereco(request.Endereco);
        person.SetAtivo(true);
        await _repository.CreateAsync(person);

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(int cpf, CancellationToken cancellationToken = default)
    {
        var person = await _repository.SearchAsync(cpf, cancellationToken);
        if (person is null)
            return Result.Fail("Falha na exclusão.");

        person.SetAtivo(false);
        await _repository.UpdateAsync(person, cancellationToken);

        return Result.Ok();
    }

    public async Task<Result<Person>> GetAsync(int cpf, CancellationToken cancellationToken = default)
    {
        if (cpf < 0)
            return Result.Fail<Person>("Falha na consulta.");

        return await _repository.SearchAsync(cpf, cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdatePersonRequest request, CancellationToken cancellationToken = default)
    {
        await request.ValidateAsync();
        if (request.IsValid)
            return request.ToFail();

        var person = await _repository.SearchAsync(request.Cpf, cancellationToken);
        if (person is null)
            return Result.Fail("Falha na atualização");

        person.SetNome(request.Nome);
        person.SetEndereco(request.Endereco);
        await _repository.UpdateAsync(person);
        
        return Result.Ok();
    }
}
