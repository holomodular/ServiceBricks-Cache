﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceBricks.Cache.SqlServer;

#nullable disable

namespace ServiceBricks.Cache.SqlServer.Migrations
{
    [DbContext(typeof(CacheSqlServerContext))]
    [Migration("20240913121352_CacheV1")]
    partial class CacheV1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Cache")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServiceBricks.Cache.EntityFrameworkCore.CacheData", b =>
                {
                    b.Property<string>("CacheKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CacheValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ExpirationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("UpdateDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("CacheKey");

                    b.HasIndex("ExpirationDate");

                    b.ToTable("CacheDatas", "Cache");
                });
#pragma warning restore 612, 618
        }
    }
}
