using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlossomHotel.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migupdateentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_WorkLocations_WorkLocationId",
                table: "Staffs");

            migrationBuilder.DropTable(
                name: "WorkLocations");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_WorkLocationId",
                table: "Staffs");

            migrationBuilder.RenameColumn(
                name: "WorkLocationId",
                table: "Staffs",
                newName: "HotelIdId");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Staffs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_HotelId",
                table: "Staffs",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Hotels_HotelId",
                table: "Staffs",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Hotels_HotelId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_HotelId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Staffs");

            migrationBuilder.RenameColumn(
                name: "HotelIdId",
                table: "Staffs",
                newName: "WorkLocationId");

            migrationBuilder.CreateTable(
                name: "WorkLocations",
                columns: table => new
                {
                    WorkLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkLocationCity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocations", x => x.WorkLocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_WorkLocationId",
                table: "Staffs",
                column: "WorkLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_WorkLocations_WorkLocationId",
                table: "Staffs",
                column: "WorkLocationId",
                principalTable: "WorkLocations",
                principalColumn: "WorkLocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
