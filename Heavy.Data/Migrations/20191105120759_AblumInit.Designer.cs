﻿// <auto-generated />
using System;
using Heavy.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Heavy.Data.Migrations
{
    [DbContext(typeof(HeavyContext))]
    [Migration("20191105120759_AblumInit")]
    partial class AblumInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Heavy.Domain.Model.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnName("create_date_time");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnName("img_url")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Title")
                        .HasColumnName("title")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("album");
                });
#pragma warning restore 612, 618
        }
    }
}
