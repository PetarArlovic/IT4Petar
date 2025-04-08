using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSApi.Migrations
{
    /// <inheritdoc />
    public partial class First_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KUPAC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SIFRA = table.Column<int>(type: "int", nullable: false),
                    NAZIV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ADRESA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MJESTO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KUPAC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROIZVOD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SIFRA = table.Column<int>(type: "int", nullable: false),
                    NAZIV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JEDINICA_MJERE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CIJENA = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    STANJE = table.Column<int>(type: "int", nullable: false),
                    PROIZVODSlikaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROIZVOD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZAGLAVLJE_RACUNA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BROJ = table.Column<int>(type: "int", nullable: false),
                    DATUM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NAPOMENA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KUPACId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZAGLAVLJE_RACUNA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZAGLAVLJE_RACUNA_KUPAC_KUPACId",
                        column: x => x.KUPACId,
                        principalTable: "KUPAC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAVKE_RACUNA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KOLICINA = table.Column<int>(type: "int", nullable: false),
                    CIJENA = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    POPUST = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    PROIZVODId = table.Column<int>(type: "int", nullable: false),
                    ZAGLAVLJE_RACUNAId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAVKE_RACUNA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAVKE_RACUNA_PROIZVOD_PROIZVODId",
                        column: x => x.PROIZVODId,
                        principalTable: "PROIZVOD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAVKE_RACUNA_ZAGLAVLJE_RACUNA_ZAGLAVLJE_RACUNAId",
                        column: x => x.ZAGLAVLJE_RACUNAId,
                        principalTable: "ZAGLAVLJE_RACUNA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KUPAC_SIFRA",
                table: "KUPAC",
                column: "SIFRA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROIZVOD_SIFRA",
                table: "PROIZVOD",
                column: "SIFRA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STAVKE_RACUNA_PROIZVODId",
                table: "STAVKE_RACUNA",
                column: "PROIZVODId");

            migrationBuilder.CreateIndex(
                name: "IX_STAVKE_RACUNA_ZAGLAVLJE_RACUNAId",
                table: "STAVKE_RACUNA",
                column: "ZAGLAVLJE_RACUNAId");

            migrationBuilder.CreateIndex(
                name: "IX_ZAGLAVLJE_RACUNA_BROJ",
                table: "ZAGLAVLJE_RACUNA",
                column: "BROJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZAGLAVLJE_RACUNA_KUPACId",
                table: "ZAGLAVLJE_RACUNA",
                column: "KUPACId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STAVKE_RACUNA");

            migrationBuilder.DropTable(
                name: "PROIZVOD");

            migrationBuilder.DropTable(
                name: "ZAGLAVLJE_RACUNA");

            migrationBuilder.DropTable(
                name: "KUPAC");
        }
    }
}
