using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGameRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participant",
                table: "GameRooms");

            migrationBuilder.AddColumn<bool>(
                name: "CreatorConnected",
                table: "GameRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ParticipantConnected",
                table: "GameRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantId",
                table: "GameRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5248eada-8691-49cd-9f33-17b06713d160", "169a6220-7e1e-4692-bbcd-c27258929aa4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "25c81989-5d86-456e-a94b-803c20c0edcb", "ca892d67-4e28-4951-8584-c4d98aa6b217" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorConnected",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "ParticipantConnected",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "GameRooms");

            migrationBuilder.AddColumn<string>(
                name: "Participant",
                table: "GameRooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f27f223-0c36-41fe-8d3c-7cfc48e2e016", "6d16b465-afbc-4c93-8226-3ac23d0166e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "38d39962-49e4-4595-b4ae-717087b1e17f", "c8adf9cd-32da-4949-9ab2-e61e8f191402" });
        }
    }
}
