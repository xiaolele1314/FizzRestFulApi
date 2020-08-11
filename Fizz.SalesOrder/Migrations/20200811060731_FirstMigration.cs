using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fizz.SalesOrder.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CreateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreateUserDate = table.Column<DateTime>(nullable: false),
                    UpdateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdateUserDate = table.Column<DateTime>(nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SignDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "detail",
                columns: table => new
                {
                    OrderNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    RowNo = table.Column<int>(nullable: false),
                    CreateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreateUserDate = table.Column<DateTime>(nullable: false),
                    UpdateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdateUserDate = table.Column<DateTime>(nullable: false),
                    MaterialNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SortNo = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detail", x => new { x.RowNo, x.OrderNo });
                    table.ForeignKey(
                        name: "FK_detail_order_OrderNo",
                        column: x => x.OrderNo,
                        principalTable: "order",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detail_OrderNo",
                table: "detail",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_detail_RowNo_OrderNo",
                table: "detail",
                columns: new[] { "RowNo", "OrderNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_No",
                table: "order",
                column: "No",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detail");

            migrationBuilder.DropTable(
                name: "order");
        }
    }
}
