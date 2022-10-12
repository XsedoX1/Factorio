using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactorioHelper.Models.Migrations
{
    public partial class ver17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_ItemId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_MainItemId",
                table: "Ingredients");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_ItemId",
                table: "Ingredients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_MainItemId",
                table: "Ingredients",
                column: "MainItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_ItemId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_MainItemId",
                table: "Ingredients");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_ItemId",
                table: "Ingredients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_MainItemId",
                table: "Ingredients",
                column: "MainItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }
    }
}
