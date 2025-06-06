﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ContextApp))]
    [Migration("20250524061848_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Persistence.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IsoCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nchar(2)")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IsoCode")
                        .IsUnique();

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("Persistence.Entities.CountryIndicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("MacroIndicatorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MacroIndicatorId");

                    b.HasIndex("CountryId", "MacroIndicatorId", "Year")
                        .IsUnique();

                    b.ToTable("CountryIndicators", (string)null);
                });

            modelBuilder.Entity("Persistence.Entities.MacroIndicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsHigherBetter")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(5,4)");

                    b.HasKey("Id");

                    b.ToTable("MacroIndicators", (string)null);
                });

            modelBuilder.Entity("Persistence.Entities.ReturnRateConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("MaxRate")
                        .HasColumnType("decimal(5,2)");

                    b.Property<decimal>("MinRate")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("ReturnRateConfigs", (string)null);
                });

            modelBuilder.Entity("Persistence.Entities.SimulationMacroIndicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MacroIndicatorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(5,4)");

                    b.HasKey("Id");

                    b.HasIndex("MacroIndicatorId")
                        .IsUnique();

                    b.ToTable("SimulationMacroIndicators", (string)null);
                });

            modelBuilder.Entity("Persistence.Entities.CountryIndicator", b =>
                {
                    b.HasOne("Persistence.Entities.Country", "Country")
                        .WithMany("Indicators")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Entities.MacroIndicator", "MacroIndicator")
                        .WithMany("CountryIndicators")
                        .HasForeignKey("MacroIndicatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("MacroIndicator");
                });

            modelBuilder.Entity("Persistence.Entities.SimulationMacroIndicator", b =>
                {
                    b.HasOne("Persistence.Entities.MacroIndicator", "MacroIndicator")
                        .WithMany("SimulationEntries")
                        .HasForeignKey("MacroIndicatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MacroIndicator");
                });

            modelBuilder.Entity("Persistence.Entities.Country", b =>
                {
                    b.Navigation("Indicators");
                });

            modelBuilder.Entity("Persistence.Entities.MacroIndicator", b =>
                {
                    b.Navigation("CountryIndicators");

                    b.Navigation("SimulationEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
