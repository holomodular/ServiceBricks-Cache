using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBricks.Cache.Sqlite.Migrations
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
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UpdateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ExpirationDate = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
