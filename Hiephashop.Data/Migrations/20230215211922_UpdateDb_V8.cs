using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiephashop.Data.Migrations
{
    public partial class UpdateDb_V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content1",
                table: "SettingLayouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content2",
                table: "SettingLayouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content3",
                table: "SettingLayouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title1",
                table: "SettingLayouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title2",
                table: "SettingLayouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title3",
                table: "SettingLayouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content1",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "Content2",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "Content3",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "Title1",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "Title2",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "Title3",
                table: "SettingLayouts");
        }
    }
}
