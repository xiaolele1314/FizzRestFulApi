using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Fizz.SalesOrder.Migrations.OrderDetail
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
                    UpdaeUserDate = table.Column<DateTime>(nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SignDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
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
                    ProNo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreateUserDate = table.Column<DateTime>(nullable: false),
                    UpdateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdaeUserDate = table.Column<DateTime>(nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaterialNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SortNo = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detail", x => x.ProNo);
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
                name: "IX_detail_ProNo",
                table: "detail",
                column: "ProNo",
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
