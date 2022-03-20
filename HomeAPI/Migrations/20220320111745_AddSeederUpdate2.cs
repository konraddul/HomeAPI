using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAPI.Migrations
{
    public partial class AddSeederUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Borrows_BorrowId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Borrows_BorrowId",
                table: "Books",
                column: "BorrowId",
                principalTable: "Borrows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Borrows_BorrowId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Borrows_BorrowId",
                table: "Books",
                column: "BorrowId",
                principalTable: "Borrows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
