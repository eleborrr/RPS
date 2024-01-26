using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstUserMoveId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondUserMoveId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "03c1a771-57db-4eb7-9b4d-ae91b87af83e", "d51b7907-c1bb-4407-9922-87f67196ea59" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "67a25b5a-2f89-479a-bf2f-90716e544241", "fc357142-d51b-45fd-93b4-1218946d4217" });
        }
    }
}
