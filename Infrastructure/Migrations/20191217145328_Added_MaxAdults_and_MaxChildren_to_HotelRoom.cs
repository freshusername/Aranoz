using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Added_MaxAdults_and_MaxChildren_to_HotelRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxAdults",
                table: "HotelRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxChildren",
                table: "HotelRooms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxAdults",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "MaxChildren",
                table: "HotelRooms");
        }
    }
}
