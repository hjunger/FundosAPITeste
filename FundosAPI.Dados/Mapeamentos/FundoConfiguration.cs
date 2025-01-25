using FundosAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundosAPI.Dados.Mapeamentos
{
    public class FundoConfiguration : IEntityTypeConfiguration<Fundo>
    {
        public void Configure(EntityTypeBuilder<Fundo> builder)
        {
            builder.
                HasIndex("Cnpj").IsUnique();
        }
    }
}
