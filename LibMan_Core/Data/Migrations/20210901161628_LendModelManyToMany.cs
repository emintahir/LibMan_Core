using Microsoft.EntityFrameworkCore.Migrations;

namespace LibMan_Core.Data.Migrations
{
    public partial class LendModelManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Borrowers_BorrowerId",
                table: "Lends");

            migrationBuilder.DropTable(
                name: "LendDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Borrowers",
                table: "Borrowers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Borrowers");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Lends",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BorrowerId",
                table: "Borrowers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Borrowers",
                table: "Borrowers",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lends_BookId",
                table: "Lends",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Books_BookId",
                table: "Lends",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Borrowers_BorrowerId",
                table: "Lends",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "BorrowerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Books_BookId",
                table: "Lends");

            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Borrowers_BorrowerId",
                table: "Lends");

            migrationBuilder.DropIndex(
                name: "IX_Lends_BookId",
                table: "Lends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Borrowers",
                table: "Borrowers");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Lends");

            migrationBuilder.DropColumn(
                name: "BorrowerId",
                table: "Borrowers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Borrowers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Borrowers",
                table: "Borrowers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LendDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    LendId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LendDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LendDetails_Lends_LendId",
                        column: x => x.LendId,
                        principalTable: "Lends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LendDetails_BookId",
                table: "LendDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_LendDetails_LendId",
                table: "LendDetails",
                column: "LendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Borrowers_BorrowerId",
                table: "Lends",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
