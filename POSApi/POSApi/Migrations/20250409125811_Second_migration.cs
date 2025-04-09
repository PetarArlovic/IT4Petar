using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSApi.Migrations
{
    /// <inheritdoc />
    public partial class Second_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAZIV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ROLE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_EMAIL",
                table: "USER",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
