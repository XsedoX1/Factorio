using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactorioHelper.Models.Migrations
{
    public partial class ver11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                columns: new[] { "AmountNeeded", "TimeToCraftMainItem", "ItemId", "MainItemId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                columns: new[] { "AmountNeeded", "TimeToCraftMainItem", "ItemId" });
        }
    }
}
