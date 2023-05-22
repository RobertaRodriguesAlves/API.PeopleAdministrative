using API.PeopleAdministrative.Domain.Entities;
using API.PeopleAdministrative.Shared.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Domain.Interfaces;

public interface IPeopleRepository : IRepository
{
    Task CreateAsync(Person people, CancellationToken cancellationToken = default);
    Task UpdateAsync(Person people, CancellationToken cancellationToken = default);
    Task<Person> SearchAsync(long cpf, CancellationToken cancellationToken = default);
}
