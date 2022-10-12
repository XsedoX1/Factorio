using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactorioHelper.Models.Migrations
{
    public partial class ver9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_ItemId1",
                table: "Ingredients");

            migrationBuilder.AlterColumn<long>(
                name: "ItemId1",
                table: "Ingredients",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_ItemId1",
                table: "Ingredients",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_ItemId1",
                table: "Ingredients");

            migrationBuilder.AlterColumn<long>(
                name: "ItemId1",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_ItemId1",
                table: "Ingredients",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
