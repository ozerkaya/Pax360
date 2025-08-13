using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pax360DAL.Migrations
{
    /// <inheritdoc />
    public partial class ver1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authorizations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Module_Users = table.Column<bool>(type: "bit", nullable: false),
                    Module_Role = table.Column<bool>(type: "bit", nullable: false),
                    Module_IsEmirleri = table.Column<bool>(type: "bit", nullable: false),
                    Module_Loglar = table.Column<bool>(type: "bit", nullable: false),
                    Module_DosyaYukleme = table.Column<bool>(type: "bit", nullable: false),
                    Module_Malzeme = table.Column<bool>(type: "bit", nullable: false),
                    Module_ManuelIsEmri = table.Column<bool>(type: "bit", nullable: false),
                    Module_Tatiller = table.Column<bool>(type: "bit", nullable: false),
                    Module_TeknisyenAdresleri = table.Column<bool>(type: "bit", nullable: false),
                    Module_Raporlar = table.Column<bool>(type: "bit", nullable: false),
                    Module_Partner = table.Column<bool>(type: "bit", nullable: false),
                    Module_Uygulamalar = table.Column<bool>(type: "bit", nullable: false),
                    Module_Raporlar_Portfoy = table.Column<bool>(type: "bit", nullable: false),
                    Module_Raporlar_Bolgesel = table.Column<bool>(type: "bit", nullable: false),
                    Module_SatisZiyaret_Listesi = table.Column<bool>(type: "bit", nullable: false),
                    Module_Satis_Listesi = table.Column<bool>(type: "bit", nullable: false),
                    Module_Fiyatlar = table.Column<bool>(type: "bit", nullable: false),
                    Module_Kargo = table.Column<bool>(type: "bit", nullable: false),
                    Module_PropaySatis = table.Column<bool>(type: "bit", nullable: false),
                    Module_PropaySatisList = table.Column<bool>(type: "bit", nullable: false),
                    Module_TaksidePosGiris = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorizations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module_Users = table.Column<bool>(type: "bit", nullable: false),
                    Module_Role = table.Column<bool>(type: "bit", nullable: false),
                    Module_IsEmirleri = table.Column<bool>(type: "bit", nullable: false),
                    Module_Loglar = table.Column<bool>(type: "bit", nullable: false),
                    Module_DosyaYukleme = table.Column<bool>(type: "bit", nullable: false),
                    Module_Malzeme = table.Column<bool>(type: "bit", nullable: false),
                    Module_ManuelIsEmri = table.Column<bool>(type: "bit", nullable: false),
                    Module_Tatiller = table.Column<bool>(type: "bit", nullable: false),
                    Module_TeknisyenAdresleri = table.Column<bool>(type: "bit", nullable: false),
                    Module_Raporlar = table.Column<bool>(type: "bit", nullable: false),
                    Module_Partner = table.Column<bool>(type: "bit", nullable: false),
                    Module_Uygulamalar = table.Column<bool>(type: "bit", nullable: false),
                    Module_Raporlar_Portfoy = table.Column<bool>(type: "bit", nullable: false),
                    Module_Raporlar_Bolgesel = table.Column<bool>(type: "bit", nullable: false),
                    Module_SatisZiyaret_Listesi = table.Column<bool>(type: "bit", nullable: false),
                    Module_Satis_Listesi = table.Column<bool>(type: "bit", nullable: false),
                    Module_Fiyatlar = table.Column<bool>(type: "bit", nullable: false),
                    Module_Kargo = table.Column<bool>(type: "bit", nullable: false),
                    Module_PropaySatis = table.Column<bool>(type: "bit", nullable: false),
                    Module_PropaySatisList = table.Column<bool>(type: "bit", nullable: false),
                    Module_TaksidePosGiris = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authorizations");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
