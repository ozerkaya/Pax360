using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pax360DAL.Migrations
{
    /// <inheritdoc />
    public partial class ver25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cari_Guid_Mikro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriSegmenti = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MusteriSektoru = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MagazaSayisi = table.Column<int>(type: "int", nullable: false),
                    KasaSayisi = table.Column<int>(type: "int", nullable: false),
                    AccountManager = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountManagerID = table.Column<int>(type: "int", nullable: false),
                    SatisKanali = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SonAktiviteNumarasi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SonAktiviteTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonAktiviteTipi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SonAktiviteOzeti = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SahaFirmasi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerBanks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBanks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerBanks_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCases",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerCases_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocuments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerDocuments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBanks_CustomerID",
                table: "CustomerBanks",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCases_CustomerID",
                table: "CustomerCases",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_CustomerID",
                table: "CustomerDocuments",
                column: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerBanks");

            migrationBuilder.DropTable(
                name: "CustomerCases");

            migrationBuilder.DropTable(
                name: "CustomerDocuments");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
