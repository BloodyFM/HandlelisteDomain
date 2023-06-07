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
    [Migration("20230418071829_varenavigation")]
    partial class varenavigation
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

            modelBuilder.Entity("HandlelisteDomain.VareInstance", b =>
                {
                    b.Property<int>("VareInstanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VareInstanceId"));

                    b.Property<int>("HandlelisteId")
                        .HasColumnType("int");

                    b.Property<int>("Mengde")
                        .HasColumnType("int");

                    b.Property<int>("VareId")
                        .HasColumnType("int");

                    b.HasKey("VareInstanceId");

                    b.HasIndex("HandlelisteId");

                    b.HasIndex("VareId");

                    b.ToTable("Vareinstance");
                });

            modelBuilder.Entity("HandlelisteDomain.VareInstance", b =>
                {
                    b.HasOne("HandlelisteDomain.Handleliste", null)
                        .WithMany("Varer")
                        .HasForeignKey("HandlelisteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HandlelisteDomain.Vare", "Vare")
                        .WithMany("VareInstancer")
                        .HasForeignKey("VareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vare");
                });

            modelBuilder.Entity("HandlelisteDomain.Handleliste", b =>
                {
                    b.Navigation("Varer");
                });

            modelBuilder.Entity("HandlelisteDomain.Vare", b =>
                {
                    b.Navigation("VareInstancer");
                });
#pragma warning restore 612, 618
        }
    }
}
