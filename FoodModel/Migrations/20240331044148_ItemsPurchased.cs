using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodModel.Migrations
{
    /// <inheritdoc />
    public partial class ItemsPurchased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemsPurchased",
                table: "MenuItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsPurchased",
                table: "MenuItem");
        }
    }
}
