using API.PeopleAdministrative.Domain.Entities;
using API.PeopleAdministrative.Domain.Interfaces;
using API.PeopleAdministrative.Infrastructure.Data.Context;
using API.PeopleAdministrative.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Infrastructure.Data.Repositories;

public sealed class PeopleRepository : RepositoryBase<Person>, IPeopleRepository
{
    public PeopleRepository(PeopleAdministrativeContext context)
        : base(context) { }

    public async Task CreateAsync(Person people, CancellationToken cancellationToken = default)
    {
        DbSet.Add(people);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long cpf, CancellationToken cancellationToken = default)
    {
        var people = await SearchAsync(cpf);
        if (people != null)
        {
            DbSet.Remove(people);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Person> SearchAsync(long cpf, CancellationToken cancellationToken = default)
        => await DbSet
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Cpf.Equals(cpf), cancellationToken);

    public async Task UpdateAsync(Person people, CancellationToken cancellationToken = default)
    {
        DbSet.Update(people);
        await SaveChangesAsync(cancellationToken);
    }
}
