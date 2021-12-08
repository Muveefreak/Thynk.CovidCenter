using Microsoft.EntityFrameworkCore.Migrations;

namespace Thynk.CovidCenter.Repository.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableDates_Locations_LocationId",
                table: "AvailableDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ApplicationUsers_ApplicationUserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AvailableDates_AvailableDateId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Locations_LocationID",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "TestType",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableDates_Locations_LocationId",
                table: "AvailableDates",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ApplicationUsers_ApplicationUserId",
                table: "Bookings",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AvailableDates_AvailableDateId",
                table: "Bookings",
                column: "AvailableDateId",
                principalTable: "AvailableDates",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Locations_LocationID",
                table: "Bookings",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableDates_Locations_LocationId",
                table: "AvailableDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ApplicationUsers_ApplicationUserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AvailableDates_AvailableDateId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Locations_LocationID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TestType",
                table: "Bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableDates_Locations_LocationId",
                table: "AvailableDates",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ApplicationUsers_ApplicationUserId",
                table: "Bookings",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AvailableDates_AvailableDateId",
                table: "Bookings",
                column: "AvailableDateId",
                principalTable: "AvailableDates",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Locations_LocationID",
                table: "Bookings",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "ID");
        }
    }
}
