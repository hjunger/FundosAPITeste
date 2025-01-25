using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundosAPI.Dados.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fundos",
                columns: table => new
                {
                    FundoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundoNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Administrador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundos", x => x.FundoId);
                });

            migrationBuilder.CreateTable(
                name: "Cotas",
                columns: table => new
                {
                    CotaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCota = table.Column<DateOnly>(type: "date", nullable: false),
                    ValorCota = table.Column<double>(type: "float", nullable: false),
                    FundoId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotas", x => x.CotaId);
                    table.ForeignKey(
                        name: "FK_Cotas_Fundos_FundoId",
                        column: x => x.FundoId,
                        principalTable: "Fundos",
                        principalColumn: "FundoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotas_FundoId",
                table: "Cotas",
                column: "FundoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotas");

            migrationBuilder.DropTable(
                name: "Fundos");
        }
    }
}
