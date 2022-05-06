using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaClient.Migrations
{
    public partial class Screenings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Rooms_RoomId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Tickets",
                newName: "ScreeningId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_RoomId",
                table: "Tickets",
                newName: "IX_Tickets_ScreeningId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentScreeningId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Screenings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "MovieId", "RoomId", "Time" },
                values: new object[] { 1, 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CurrentScreeningId",
                table: "Rooms",
                column: "CurrentScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_RoomId",
                table: "Screenings",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Screenings_CurrentScreeningId",
                table: "Rooms",
                column: "CurrentScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Screenings_ScreeningId",
                table: "Tickets",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Screenings_CurrentScreeningId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Screenings_ScreeningId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CurrentScreeningId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CurrentScreeningId",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "ScreeningId",
                table: "Tickets",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ScreeningId",
                table: "Tickets",
                newName: "IX_Tickets_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Rooms_RoomId",
                table: "Tickets",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
