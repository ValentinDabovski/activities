using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UseDomainModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Address_Venue",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Category_Description",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Venue",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category_Description",
                table: "Activities",
                type: "TEXT",
                nullable: true);
        }
    }
}
