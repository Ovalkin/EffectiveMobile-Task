﻿// <auto-generated />
using System;
using EffectiveMobile.Common.EntityModel.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EffectiveMobile.Common.EntityModel.Sqlite.Migrations
{
    [DbContext(typeof(EffectiveMobileContext))]
    partial class EffectiveMobileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("EffectiveMobile.Common.EntityModel.Sqlite.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("CityDistrict")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("city_district");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("delivery_time");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.ToTable("orders");
                });
#pragma warning restore 612, 618
        }
    }
}