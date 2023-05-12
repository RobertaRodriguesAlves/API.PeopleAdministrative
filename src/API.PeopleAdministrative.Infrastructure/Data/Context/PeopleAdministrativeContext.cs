using API.PeopleAdministrative.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.PeopleAdministrative.Infrastructure.Data.Context;

public sealed class PeopleAdministrativeContext : DbContext
{
	public PeopleAdministrativeContext(DbContextOptions<PeopleAdministrativeContext> options) : base(options)
	{
		// Desabilita JOIN automático.
		ChangeTracker.LazyLoadingEnabled = false;
	}

	public DbSet<Person> People => Set<Person>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
		=> modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
