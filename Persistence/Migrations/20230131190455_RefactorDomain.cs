using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Venue",
                table: "Activities",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Activities",
                newName: "AddressId");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    Venue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AddressId",
                table: "Activities",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CategoryId",
                table: "Activities",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Address_AddressId",
                table: "Activities",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Category_CategoryId",
                table: "Activities",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Address_AddressId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Category_CategoryId",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AddressId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CategoryId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Activities",
                newName: "Venue");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Activities",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Activities",
                type: "TEXT",
                nullable: true);
        }
    }
}
