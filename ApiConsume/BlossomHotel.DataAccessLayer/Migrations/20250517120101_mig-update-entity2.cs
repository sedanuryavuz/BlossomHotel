using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlossomHotel.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migupdateentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Hotels_HotelId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "HotelIdId",
                table: "Staffs");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Hotels_HotelId",
                table: "Staffs",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Hotels_HotelId",
                table: "Staffs");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Staffs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HotelIdId",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Hotels_HotelId",
                table: "Staffs",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");
        }
    }
}
