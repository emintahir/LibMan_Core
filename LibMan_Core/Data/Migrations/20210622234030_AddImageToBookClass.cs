using Microsoft.EntityFrameworkCore.Migrations;

namespace LibMan_Core.Data.Migrations
{
    public partial class AddImageToBookClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAddress",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "ImageAddress",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
