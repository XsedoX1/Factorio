using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactorioHelper.Models.Migrations
{
    public partial class ver10test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_ItemId1",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_ItemId1",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ItemId1",
                table: "Ingredients");

            migrationBuilder.AddColumn<long>(
                name: "MainItemId",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_MainItemId",
                table: "Ingredients",
                column: "MainItemId");

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
                name: "FK_Ingredients_Items_MainItemId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_MainItemId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "MainItemId",
                table: "Ingredients");

            migrationBuilder.AddColumn<long>(
                name: "ItemId1",
                table: "Ingredients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ItemId1",
                table: "Ingredients",
                column: "ItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_ItemId1",
                table: "Ingredients",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "ItemId");
        }
    }
}
