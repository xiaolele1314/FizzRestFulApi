using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Fizz.SalesOrder.Migrations.OrderDetail
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
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
                    table.PrimaryKey("PK_Order", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "orderDetails",
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
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetails", x => x.ProNo);
                    table.ForeignKey(
                        name: "FK_orderDetails_Order_No",
                        column: x => x.No,
                        principalTable: "Order",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_No",
                table: "orderDetails",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_ProNo",
                table: "orderDetails",
                column: "ProNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderDetails");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
