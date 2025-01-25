using FundosAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundosAPI.Dados.Mapeamentos
{
    public class CotaFundoConfiguration : IEntityTypeConfiguration<CotaFundo>
    {
        public void Configure(EntityTypeBuilder<CotaFundo> builder)
        {
            builder
                .HasIndex(c => new { c.FundoId, c.DataCota })
                .IsUnique(true);

            builder
                .HasOne(e => e.Fundo)
                .WithMany(e => e.CotasDoFundo)
                .HasForeignKey(e => e.FundoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
