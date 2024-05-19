using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodModel.Migrations
{
    /// <inheritdoc />
    public partial class PlaceInitials2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceInitials2",
                table: "FoodPlace",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceInitials2",
                table: "FoodPlace");
        }
    }
}
