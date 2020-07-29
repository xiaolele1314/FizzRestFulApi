using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TodoApi.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orderDetails",
                columns: table => new
                {
                    ProNo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    No = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaterialNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SortNo = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    CreatNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    UpdateNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdaeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetails", x => x.ProNo);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SignDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    CreatNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    UpdateNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdaeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.No);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_ProNo",
                table: "orderDetails",
                column: "ProNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_No",
                table: "orders",
                column: "No",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderDetails");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
