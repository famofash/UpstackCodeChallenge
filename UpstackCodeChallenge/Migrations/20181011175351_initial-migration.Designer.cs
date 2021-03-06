﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpstackCodeChallenge.Data;

namespace UpstackCodeChallenge.Migrations
{
    [DbContext(typeof(UpstackDbContext))]
    [Migration("20181011175351_initial-migration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UpstackCodeChallenge.Data.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientURL");

                    b.Property<string>("Email");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("Token");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Tbl_UserRegistration");
                });
#pragma warning restore 612, 618
        }
    }
}
