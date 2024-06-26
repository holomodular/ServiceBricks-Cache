﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceBricks.Cache.Sqlite;

#nullable disable

namespace ServiceBricks.Cache.Sqlite.Migrations
{
    [DbContext(typeof(CacheSqliteContext))]
    [Migration("20240221025611_CacheV1")]
    partial class CacheV1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Cache")
                .HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ServiceBricks.Cache.EntityFrameworkCore.CacheData", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("CreateDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("ExpirationDate")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("UpdateDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("CacheData", "Cache");
                });
#pragma warning restore 612, 618
        }
    }
}
