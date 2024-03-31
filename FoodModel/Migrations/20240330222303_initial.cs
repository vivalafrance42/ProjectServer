using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodModel.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodPlace",
                columns: table => new
                {
                    FoodPlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FoodPlace", x => x.FoodPlaceId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodPlaceId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuItem__8943F7227526083B", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItem_FoodPlace",
                        column: x => x.FoodPlaceId,
                        principalTable: "FoodPlace",
                        principalColumn: "FoodPlaceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_FoodPlaceId",
                table: "MenuItem",
                column: "FoodPlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "FoodPlace");
        }
    }
}
