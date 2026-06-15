using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CornerStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cashiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierId = table.Column<int>(type: "integer", nullable: false),
                    PaidOnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cashiers_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Cashiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cashiers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Alice", "Johnson" },
                    { 2, "Brian", "Martinez" },
                    { 3, "Carol", "Thompson" },
                    { 4, "David", "Lee" },
                    { 5, "Maria", "Garcia" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Snacks" },
                    { 2, "Beverages" },
                    { 3, "Magazines" },
                    { 4, "Frozen Foods" },
                    { 5, "Candy" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CashierId", "OrderId", "PaidOnDate" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, null, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, null, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 4, null, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 5, null, new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, "Lays", 1, 1.99m, "Potato Chips" },
                    { 2, "Fritos", 1, 2.49m, "Corn Chips" },
                    { 3, "Coca-Cola", 2, 1.79m, "Cola" },
                    { 4, "Tropicana", 2, 2.99m, "Orange Juice" },
                    { 5, "SI", 3, 5.99m, "Sports Illustrated" },
                    { 6, "El Monterey", 4, 3.49m, "Frozen Burrito" },
                    { 7, "Haribo", 5, 1.49m, "Gummy Bears" },
                    { 8, "Hershey's", 5, 1.29m, "Chocolate Bar" },
                    { 9, "Red Bull", 2, 3.29m, "Energy Drink" },
                    { 10, "DiGiorno", 4, 5.49m, "Frozen Pizza" }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 2m },
                    { 1, 3, 1m },
                    { 2, 7, 3m },
                    { 2, 9, 2m },
                    { 3, 4, 2m },
                    { 3, 5, 1m },
                    { 4, 6, 1m },
                    { 4, 8, 2m },
                    { 5, 10, 1m },
                    { 6, 2, 1m },
                    { 6, 3, 3m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierId",
                table: "Orders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderId",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Cashiers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
