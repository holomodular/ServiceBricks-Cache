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
            migrationBuilder.CreateTable(
                name: "CacheDatas",
                columns: table => new
                {
                    CacheKey = table.Column<string>(type: "TEXT", nullable: false),
                    CacheValue = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UpdateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ExpirationDate = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheDatas", x => x.CacheKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CacheDatas_ExpirationDate",
                table: "CacheDatas",
                column: "ExpirationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheDatas");
        }
    }
}