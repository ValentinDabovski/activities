using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "TEXT", nullable: true),
                    AddressCity = table.Column<string>(name: "Address_City", type: "TEXT", nullable: true),
                    AddressState = table.Column<string>(name: "Address_State", type: "TEXT", nullable: true),
                    AddressCountry = table.Column<string>(name: "Address_Country", type: "TEXT", nullable: true),
                    AddressZipCode = table.Column<string>(name: "Address_ZipCode", type: "TEXT", nullable: true),
                    AddressVenue = table.Column<string>(name: "Address_Venue", type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
