﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReaddingMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DosesOfMilk = table.Column<int>(type: "int", nullable: false),
                    PacksOfSugar = table.Column<int>(type: "int", nullable: false),
                    Cinnamon = table.Column<bool>(type: "bit", nullable: false),
                    Stevia = table.Column<bool>(type: "bit", nullable: false),
                    CoconutMilk = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeIngredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoffeeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoffeeIngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoffeeTypes_CoffeeIngredients_CoffeeIngredientId",
                        column: x => x.CoffeeIngredientId,
                        principalTable: "CoffeeIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CoffeeIngredients",
                columns: new[] { "Id", "Cinnamon", "CoconutMilk", "DosesOfMilk", "PacksOfSugar", "Stevia" },
                values: new object[,]
                {
                    { new Guid("587cdc3e-40a8-43f1-b67e-251292d94f3e"), false, false, 1, 2, true },
                    { new Guid("ea3cf94a-7ab6-4347-a0a2-b8f32d2ba51b"), false, true, 2, 0, false },
                    { new Guid("f0ca2d3e-554f-459e-9045-dce2d5ab616b"), true, true, 0, 1, false }
                });

            migrationBuilder.InsertData(
                table: "CoffeeTypes",
                columns: new[] { "Id", "CoffeeIngredientId", "Name" },
                values: new object[,]
                {
                    { new Guid("49ed75aa-0a08-40c8-92ed-cd88a68f564d"), new Guid("587cdc3e-40a8-43f1-b67e-251292d94f3e"), "Espresso" },
                    { new Guid("5cd91938-abc4-440f-b3c1-52371516bf8d"), new Guid("f0ca2d3e-554f-459e-9045-dce2d5ab616b"), "Americano" },
                    { new Guid("8f516d6a-e1b3-4d66-b0d7-f9b40cdcdb04"), new Guid("ea3cf94a-7ab6-4347-a0a2-b8f32d2ba51b"), "Caffè Crema" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeTypes_CoffeeIngredientId",
                table: "CoffeeTypes",
                column: "CoffeeIngredientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeTypes");

            migrationBuilder.DropTable(
                name: "CoffeeIngredients");
        }
    }
}
