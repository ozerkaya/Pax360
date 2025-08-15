using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pax360DAL.Migrations
{
    /// <inheritdoc />
    public partial class ver11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicariUnvan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VKNTCKN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaturaAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Il = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ilce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaticiPlasiyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeslimatAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeslimatIl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeslimatIlce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiparisNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cari_Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cari_kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VadeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeslimTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SahaFirmasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankaOrtami = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CihazModu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entegrasyon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YuklenecekBanka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YuklenecekUygulama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Not = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrdersItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CihazModeli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miktar = table.Column<int>(type: "int", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BirimFiyatTL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kdv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iskonto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToplamTutar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrdersItem_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItem_OrderID",
                table: "OrdersItem",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersItem");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
