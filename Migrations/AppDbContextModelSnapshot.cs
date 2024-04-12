﻿// <auto-generated />
using System;
using APISensor.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APISensor.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("APISensor.Models.CSVDados", b =>
                {
                    b.Property<int>("CSVId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CSVId"));

                    b.Property<string>("DadosequipmentId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EquipmentId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("CSVId");

                    b.HasIndex("DadosequipmentId");

                    b.ToTable("CSVDados");
                });

            modelBuilder.Entity("APISensor.Models.Dados", b =>
                {
                    b.Property<string>("equipmentId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("Value")
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("equipmentId");

                    b.ToTable("Dados");
                });

            modelBuilder.Entity("APISensor.Models.CSVDados", b =>
                {
                    b.HasOne("APISensor.Models.Dados", null)
                        .WithMany("DadosSensores")
                        .HasForeignKey("DadosequipmentId");
                });

            modelBuilder.Entity("APISensor.Models.Dados", b =>
                {
                    b.Navigation("DadosSensores");
                });
#pragma warning restore 612, 618
        }
    }
}
