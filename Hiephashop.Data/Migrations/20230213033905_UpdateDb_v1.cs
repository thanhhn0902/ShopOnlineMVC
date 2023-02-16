using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiephashop.Data.Migrations
{
    public partial class UpdateDb_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "File",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_ProductCode",
                table: "File",
                column: "ProductCode");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Product_ProductCode",
                table: "File",
                column: "ProductCode",
                principalTable: "Product",
                principalColumn: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Product_ProductCode",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_ProductCode",
                table: "File");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "File",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
