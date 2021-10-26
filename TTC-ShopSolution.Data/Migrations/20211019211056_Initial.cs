using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTC_ShopSolution.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SeoAlias",
                table: "Products",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Products",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 20, 4, 10, 56, 1, DateTimeKind.Local).AddTicks(8954),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 19, 17, 15, 11, 626, DateTimeKind.Local).AddTicks(1810));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "SeoAlias",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 19, 17, 15, 11, 626, DateTimeKind.Local).AddTicks(1810),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 10, 20, 4, 10, 56, 1, DateTimeKind.Local).AddTicks(8954));
        }
    }
}
