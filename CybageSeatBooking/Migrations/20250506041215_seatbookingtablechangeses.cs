using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CybageSeatBooking.Migrations
{
    /// <inheritdoc />
    public partial class seatbookingtablechangeses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "seatBookings",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "seatBookings",
                newName: "id");
        }
    }
}
