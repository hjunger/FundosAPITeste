using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundosAPI.Dados.Migrations
{
    /// <inheritdoc />
    public partial class CriandoConstraintCotaFundo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex("IX_FundoId_DataCota", "Cotas", ["FundoId", "DataCota"], unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_FundoId_DataCota");
        }
    }
}
