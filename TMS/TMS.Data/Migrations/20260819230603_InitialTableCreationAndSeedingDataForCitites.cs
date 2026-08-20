using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialTableCreationAndSeedingDataForCitites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimezoneId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginCityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                });

            migrationBuilder.CreateTable(
                name: "RailCardEventRecords",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    EventTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailCardEventRecords", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_RailCardEventRecords_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName", "TimezoneId" },
                values: new object[,]
                {
                    { 1, "Vancouver", "Pacific Standard Time" },
                    { 2, "Victoria", "Pacific Standard Time" },
                    { 3, "Kelowna", "Pacific Standard Time" },
                    { 4, "Kamloops", "Pacific Standard Time" },
                    { 5, "Prince George", "Pacific Standard Time" },
                    { 6, "Calgary", "Mountain Standard Time" },
                    { 7, "Edmonton", "Mountain Standard Time" },
                    { 8, "Lethbridge", "Mountain Standard Time" },
                    { 9, "Red Deer", "Mountain Standard Time" },
                    { 10, "Fort McMurray", "Mountain Standard Time" },
                    { 11, "Regina", "Canada Central Standard Time" },
                    { 12, "Saskatoon", "Canada Central Standard Time" },
                    { 13, "Moose Jaw", "Canada Central Standard Time" },
                    { 14, "Brandon", "Central Standard Time" },
                    { 15, "Winnipeg", "Central Standard Time" },
                    { 16, "Thunder Bay", "Eastern Standard Time" },
                    { 17, "Sault Ste. Marie", "Eastern Standard Time" },
                    { 18, "Sudbury", "Eastern Standard Time" },
                    { 19, "North Bay", "Eastern Standard Time" },
                    { 20, "Barrie", "Eastern Standard Time" },
                    { 21, "Toronto", "Eastern Standard Time" },
                    { 22, "Mississauga", "Eastern Standard Time" },
                    { 23, "Hamilton", "Eastern Standard Time" },
                    { 24, "London", "Eastern Standard Time" },
                    { 25, "Kitchener", "Eastern Standard Time" },
                    { 26, "Windsor", "Eastern Standard Time" },
                    { 27, "St. Catharines", "Eastern Standard Time" },
                    { 28, "Oshawa", "Eastern Standard Time" },
                    { 29, "Kingston", "Eastern Standard Time" },
                    { 30, "Ottawa", "Eastern Standard Time" },
                    { 31, "Gatineau", "Eastern Standard Time" },
                    { 32, "Montreal", "Eastern Standard Time" },
                    { 33, "Quebec City", "Eastern Standard Time" },
                    { 34, "Sherbrooke", "Eastern Standard Time" },
                    { 35, "Trois-Rivières", "Eastern Standard Time" },
                    { 36, "Saguenay", "Eastern Standard Time" },
                    { 37, "Rimouski", "Eastern Standard Time" },
                    { 38, "Edmundston", "Atlantic Standard Time" },
                    { 39, "Fredericton", "Atlantic Standard Time" },
                    { 40, "Moncton", "Atlantic Standard Time" },
                    { 41, "Saint John", "Atlantic Standard Time" },
                    { 42, "Bathurst", "Atlantic Standard Time" },
                    { 43, "Charlottetown", "Atlantic Standard Time" },
                    { 44, "Summerside", "Atlantic Standard Time" },
                    { 45, "Sydney", "Atlantic Standard Time" },
                    { 46, "Truro", "Atlantic Standard Time" },
                    { 47, "New Glasgow", "Atlantic Standard Time" },
                    { 48, "Dartmouth", "Atlantic Standard Time" },
                    { 49, "Halifax", "Atlantic Standard Time" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RailCardEventRecords_TripId",
                table: "RailCardEventRecords",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "RailCardEventRecords");

            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
