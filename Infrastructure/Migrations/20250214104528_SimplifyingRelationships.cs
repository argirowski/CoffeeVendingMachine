using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyingRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeIngredientTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "CoffeeIngredientId",
                table: "CoffeeTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "CoffeeTypes",
                keyColumn: "Id",
                keyValue: new Guid("49ed75aa-0a08-40c8-92ed-cd88a68f564d"),
                column: "CoffeeIngredientId",
                value: new Guid("587cdc3e-40a8-43f1-b67e-251292d94f3e"));

            migrationBuilder.UpdateData(
                table: "CoffeeTypes",
                keyColumn: "Id",
                keyValue: new Guid("5cd91938-abc4-440f-b3c1-52371516bf8d"),
                column: "CoffeeIngredientId",
                value: new Guid("f0ca2d3e-554f-459e-9045-dce2d5ab616b"));

            migrationBuilder.UpdateData(
                table: "CoffeeTypes",
                keyColumn: "Id",
                keyValue: new Guid("8f516d6a-e1b3-4d66-b0d7-f9b40cdcdb04"),
                column: "CoffeeIngredientId",
                value: new Guid("ea3cf94a-7ab6-4347-a0a2-b8f32d2ba51b"));

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeTypes_CoffeeIngredientId",
                table: "CoffeeTypes",
                column: "CoffeeIngredientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeTypes_CoffeeIngredients_CoffeeIngredientId",
                table: "CoffeeTypes",
                column: "CoffeeIngredientId",
                principalTable: "CoffeeIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeTypes_CoffeeIngredients_CoffeeIngredientId",
                table: "CoffeeTypes");

            migrationBuilder.DropIndex(
                name: "IX_CoffeeTypes_CoffeeIngredientId",
                table: "CoffeeTypes");

            migrationBuilder.DropColumn(
                name: "CoffeeIngredientId",
                table: "CoffeeTypes");

            migrationBuilder.CreateTable(
                name: "CoffeeIngredientTypes",
                columns: table => new
                {
                    CoffeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoffeeIngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeIngredientTypes", x => new { x.CoffeeId, x.CoffeeIngredientId });
                    table.ForeignKey(
                        name: "FK_CoffeeIngredientTypes_CoffeeIngredients_CoffeeIngredientId",
                        column: x => x.CoffeeIngredientId,
                        principalTable: "CoffeeIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoffeeIngredientTypes_CoffeeTypes_CoffeeId",
                        column: x => x.CoffeeId,
                        principalTable: "CoffeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CoffeeIngredientTypes",
                columns: new[] { "CoffeeId", "CoffeeIngredientId" },
                values: new object[,]
                {
                    { new Guid("49ed75aa-0a08-40c8-92ed-cd88a68f564d"), new Guid("587cdc3e-40a8-43f1-b67e-251292d94f3e") },
                    { new Guid("5cd91938-abc4-440f-b3c1-52371516bf8d"), new Guid("f0ca2d3e-554f-459e-9045-dce2d5ab616b") },
                    { new Guid("8f516d6a-e1b3-4d66-b0d7-f9b40cdcdb04"), new Guid("ea3cf94a-7ab6-4347-a0a2-b8f32d2ba51b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeIngredientTypes_CoffeeIngredientId",
                table: "CoffeeIngredientTypes",
                column: "CoffeeIngredientId");
        }
    }
}
