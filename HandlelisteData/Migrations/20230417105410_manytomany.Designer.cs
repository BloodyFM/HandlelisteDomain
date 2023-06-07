﻿// <auto-generated />
using HandlelisteData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HandlelisteData.Migrations
{
    [DbContext(typeof(HandlelisteContext))]
    [Migration("20230417105410_manytomany")]
    partial class manytomany
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HandlelisteDomain.Handleliste", b =>
                {
                    b.Property<int>("HandlelisteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HandlelisteId"));

                    b.Property<string>("HandlelisteName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("HandlelisteId");

                    b.ToTable("Handlelister");
                });

            modelBuilder.Entity("HandlelisteDomain.Vare", b =>
                {
                    b.Property<int>("VareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VareId"));

                    b.Property<string>("VareName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VareId");

                    b.ToTable("Varer");

                    b.HasData(
                        new
                        {
                            VareId = 1,
                            VareName = "Pizza"
                        },
                        new
                        {
                            VareId = 2,
                            VareName = "Cola"
                        },
                        new
                        {
                            VareId = 3,
                            VareName = "Rømmedressing"
                        },
                        new
                        {
                            VareId = 4,
                            VareName = "Rømme"
                        });
                });

            modelBuilder.Entity("HandlelisteVare", b =>
                {
                    b.Property<int>("HandlelisterHandlelisteId")
                        .HasColumnType("int");

                    b.Property<int>("VarerVareId")
                        .HasColumnType("int");

                    b.HasKey("HandlelisterHandlelisteId", "VarerVareId");

                    b.HasIndex("VarerVareId");

                    b.ToTable("HandlelisteVare");
                });

            modelBuilder.Entity("HandlelisteVare", b =>
                {
                    b.HasOne("HandlelisteDomain.Handleliste", null)
                        .WithMany()
                        .HasForeignKey("HandlelisterHandlelisteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HandlelisteDomain.Vare", null)
                        .WithMany()
                        .HasForeignKey("VarerVareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
