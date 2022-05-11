using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Data.Migrations
{
    public partial class PasswordField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 19, 5, 35, 96, DateTimeKind.Local).AddTicks(7828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 24, 19, 29, 59, 460, DateTimeKind.Local).AddTicks(6993));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                defaultValue: "TesteTemplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 24, 19, 29, 59, 460, DateTimeKind.Local).AddTicks(6993),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 10, 19, 5, 35, 96, DateTimeKind.Local).AddTicks(7828));
        }
    }
}
