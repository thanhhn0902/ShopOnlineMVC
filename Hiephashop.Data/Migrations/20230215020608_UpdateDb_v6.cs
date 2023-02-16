using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiephashop.Data.Migrations
{
    public partial class UpdateDb_v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopName",
                table: "SettingLayouts",
                newName: "ShopNameRight");

            migrationBuilder.AddColumn<string>(
                name: "ShopNameLeft",
                table: "SettingLayouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopNameLeft",
                table: "SettingLayouts");

            migrationBuilder.RenameColumn(
                name: "ShopNameRight",
                table: "SettingLayouts",
                newName: "ShopName");
        }
    }
}
