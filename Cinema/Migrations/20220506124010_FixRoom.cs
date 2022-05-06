using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaClient.Migrations
{
    public partial class FixRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Movies_MovieId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_MovieId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Rooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "MovieId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "MovieId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_MovieId",
                table: "Rooms",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Movies_MovieId",
                table: "Rooms",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
