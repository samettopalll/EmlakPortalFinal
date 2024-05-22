using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakPortal.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lands_Categories_CategoryId",
                table: "Lands");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Properties",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                newName: "IX_Properties_CategoryID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Lands",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Lands_CategoryId",
                table: "Lands",
                newName: "IX_Lands_CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lands_Categories_CategoryID",
                table: "Lands",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryID",
                table: "Properties",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lands_Categories_CategoryID",
                table: "Lands");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryID",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Properties",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CategoryID",
                table: "Properties",
                newName: "IX_Properties_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Lands",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Lands_CategoryID",
                table: "Lands",
                newName: "IX_Lands_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lands_Categories_CategoryId",
                table: "Lands",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
