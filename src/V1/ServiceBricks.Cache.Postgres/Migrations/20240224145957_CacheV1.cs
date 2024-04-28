using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBricks.Cache.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class CacheV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cache");

            migrationBuilder.CreateTable(
                name: "CacheData",
                schema: "Cache",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheData", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheData",
                schema: "Cache");
        }
    }
}
