using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactorioHelper.Models.Migrations
{
    public partial class ver6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeToCraft = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AmountCrafted = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAssemblingMachine = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountPerSec = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    ItemId = table.Column<long>(type: "INTEGER", nullable: false),
                    AmountNeeded = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeToCraftMainItem = table.Column<double>(type: "REAL", nullable: false),
                    AmountNeededPerSec = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => new { x.AmountNeeded, x.TimeToCraftMainItem, x.ItemId });
                    table.ForeignKey(
                        name: "FK_Ingredients_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ItemId",
                table: "Ingredients",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
