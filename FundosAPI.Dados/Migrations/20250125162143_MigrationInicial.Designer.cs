﻿// <auto-generated />
using System;
using FundosAPI.Dados.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FundosAPI.Dados.Migrations
{
    [DbContext(typeof(SistemaFundoContext))]
    [Migration("20250125162143_MigrationInicial")]
    partial class MigrationInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FundosAPI.Entities.CotaFundo", b =>
                {
                    b.Property<int>("CotaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CotaId"));

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DataCota")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("FundoId")
                        .HasColumnType("int");

                    b.Property<double>("ValorCota")
                        .HasColumnType("float");

                    b.HasKey("CotaId");

                    b.HasIndex("FundoId");

                    b.ToTable("Cotas");
                });

            modelBuilder.Entity("FundosAPI.Entities.Fundo", b =>
                {
                    b.Property<int>("FundoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FundoId"));

                    b.Property<string>("Administrador")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly?>("DataFim")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataInicio")
                        .HasColumnType("date");

                    b.Property<string>("FundoNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FundoId");

                    b.ToTable("Fundos");
                });

            modelBuilder.Entity("FundosAPI.Entities.CotaFundo", b =>
                {
                    b.HasOne("FundosAPI.Entities.Fundo", "Fundo")
                        .WithMany("CotasDoFundo")
                        .HasForeignKey("FundoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fundo");
                });

            modelBuilder.Entity("FundosAPI.Entities.Fundo", b =>
                {
                    b.Navigation("CotasDoFundo");
                });
#pragma warning restore 612, 618
        }
    }
}
