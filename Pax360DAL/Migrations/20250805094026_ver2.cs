using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pax360DAL.Migrations
{
    /// <inheritdoc />
    public partial class ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Users",
                newName: "MikroCompanyName");

            migrationBuilder.AddColumn<Guid>(
                name: "MikroCompanyID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MikroCompanyID",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MikroCompanyName",
                table: "Users",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
