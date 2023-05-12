using API.PeopleAdministrative.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.PeopleAdministrative.Infrastructure.Data.Configurations;

public sealed class PeopleConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Cpf);

        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(11)
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(255);

        builder.Property(p => p.Endereco)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(200);

        builder.Property(p => p.Ativo)
            .IsRequired();

        builder.HasQueryFilter(p => p.Ativo);
    }
}
