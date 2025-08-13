using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pax360DAL.Migrations
{
    /// <inheritdoc />
    public partial class ver5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Module_DosyaYukleme",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Fiyatlar",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_IsEmirleri",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Kargo",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Loglar",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Malzeme",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_ManuelIsEmri",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Partner",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_PropaySatis",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_PropaySatisList",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Raporlar",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Raporlar_Bolgesel",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Raporlar_Portfoy",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_SatisZiyaret_Listesi",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Satis_Listesi",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_TaksidePosGiris",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_Tatiller",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_TeknisyenAdresleri",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "Module_DosyaYukleme",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Fiyatlar",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_IsEmirleri",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Kargo",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Loglar",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Malzeme",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_ManuelIsEmri",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Partner",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_PropaySatis",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_PropaySatisList",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Raporlar",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Raporlar_Bolgesel",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Raporlar_Portfoy",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_SatisZiyaret_Listesi",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Satis_Listesi",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_TaksidePosGiris",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_Tatiller",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "Module_TeknisyenAdresleri",
                table: "Authorizations");

            migrationBuilder.RenameColumn(
                name: "Module_Uygulamalar",
                table: "RoleTypes",
                newName: "Module_Order");

            migrationBuilder.RenameColumn(
                name: "Module_Uygulamalar",
                table: "Authorizations",
                newName: "Module_Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Module_Order",
                table: "RoleTypes",
                newName: "Module_Uygulamalar");

            migrationBuilder.RenameColumn(
                name: "Module_Order",
                table: "Authorizations",
                newName: "Module_Uygulamalar");

            migrationBuilder.AddColumn<bool>(
                name: "Module_DosyaYukleme",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Fiyatlar",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_IsEmirleri",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Kargo",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Loglar",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Malzeme",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_ManuelIsEmri",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Partner",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_PropaySatis",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_PropaySatisList",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Raporlar",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Raporlar_Bolgesel",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Raporlar_Portfoy",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_SatisZiyaret_Listesi",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Satis_Listesi",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_TaksidePosGiris",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Tatiller",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_TeknisyenAdresleri",
                table: "RoleTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_DosyaYukleme",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Fiyatlar",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_IsEmirleri",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Kargo",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Loglar",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Malzeme",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_ManuelIsEmri",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Partner",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_PropaySatis",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_PropaySatisList",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Raporlar",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Raporlar_Bolgesel",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Raporlar_Portfoy",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_SatisZiyaret_Listesi",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Satis_Listesi",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_TaksidePosGiris",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_Tatiller",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Module_TeknisyenAdresleri",
                table: "Authorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
