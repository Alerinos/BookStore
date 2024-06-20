using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookStory_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookStore.Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Bookstand = table.Column<int>(type: "int", nullable: false),
                    Shelf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStore.Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookStore.Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStore.Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookStore.Author",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStore.Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookStore.Author_BookStore.Book_BookId",
                        column: x => x.BookId,
                        principalTable: "BookStore.Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookStore.OrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStore.OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookStore.OrderLine_BookStore.Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "BookStore.Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookStore.Author_BookId",
                table: "BookStore.Author",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookStore.Book_Title",
                table: "BookStore.Book",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookStore.OrderLine_OrderId",
                table: "BookStore.OrderLine",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookStore.Author");

            migrationBuilder.DropTable(
                name: "BookStore.OrderLine");

            migrationBuilder.DropTable(
                name: "BookStore.Book");

            migrationBuilder.DropTable(
                name: "BookStore.Order");
        }
    }
}
