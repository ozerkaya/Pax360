using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pax360DAL.Migrations
{
    /// <inheritdoc />
    public partial class ver9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adet",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Fiyat",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "UrunKodu",
                table: "Offers",
                newName: "cari_kod");

            migrationBuilder.RenameColumn(
                name: "UrunAdi",
                table: "Offers",
                newName: "TeklifStatus");

            migrationBuilder.AddColumn<string>(
                name: "MusteriAdi",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OffersItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffersItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OffersItem_Offers_OfferID",
                        column: x => x.OfferID,
                        principalTable: "Offers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OffersItem_OfferID",
                table: "OffersItem",
                column: "OfferID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OffersItem");

            migrationBuilder.DropColumn(
                name: "MusteriAdi",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "cari_kod",
                table: "Offers",
                newName: "UrunKodu");

            migrationBuilder.RenameColumn(
                name: "TeklifStatus",
                table: "Offers",
                newName: "UrunAdi");

            migrationBuilder.AddColumn<int>(
                name: "Adet",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Fiyat",
                table: "Offers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
