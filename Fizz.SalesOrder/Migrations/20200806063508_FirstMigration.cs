using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Fizz.SalesOrder.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CreatSaleNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatSaleDate = table.Column<DateTime>(nullable: false),
                    UpdateSaleNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdaeSaleDate = table.Column<DateTime>(nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SignDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    ProNo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatSaleNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatSaleDate = table.Column<DateTime>(nullable: false),
                    UpdateSaleNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdaeSaleDate = table.Column<DateTime>(nullable: false),
                    No = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaterialNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SortNo = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    OrderNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.ProNo);
                    table.ForeignKey(
                        name: "FK_OrderDetail_orders_OrderNo",
                        column: x => x.OrderNo,
                        principalTable: "orders",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderNo",
                table: "OrderDetail",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_orders_No",
                table: "orders",
                column: "No",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
