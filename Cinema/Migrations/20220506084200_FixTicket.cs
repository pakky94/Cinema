using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaClient.Migrations
{
    public partial class FixTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spectators_Tickets_TicketId",
                table: "Spectators");

            migrationBuilder.DropIndex(
                name: "IX_Spectators_TicketId",
                table: "Spectators");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Spectators");

            migrationBuilder.AddColumn<int>(
                name: "SpectatorId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "SpectatorId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SpectatorId",
                table: "Tickets",
                column: "SpectatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Spectators_SpectatorId",
                table: "Tickets",
                column: "SpectatorId",
                principalTable: "Spectators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Spectators_SpectatorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SpectatorId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SpectatorId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Spectators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Spectators",
                keyColumn: "Id",
                keyValue: 1,
                column: "TicketId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Spectators_TicketId",
                table: "Spectators",
                column: "TicketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Spectators_Tickets_TicketId",
                table: "Spectators",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
