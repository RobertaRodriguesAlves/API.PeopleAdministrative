using API.PeopleAdministrative.Application.Requests;
using API.PeopleAdministrative.Domain.Entities;
using API.PeopleAdministrative.Shared.Abstractions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Application.Interfaces;

public interface IAdminService : IAppService
{
    Task<Result<Person>> GetAsync(int cpf, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(UpdatePersonRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int cpf, CancellationToken cancellationToken = default);
    Task<Result> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default);
}
