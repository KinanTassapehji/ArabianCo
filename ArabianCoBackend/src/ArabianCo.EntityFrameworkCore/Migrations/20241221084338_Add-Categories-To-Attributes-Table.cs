using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArabianCo.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesToAttributesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Categories_CategoryId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CategoryId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Attributes");

            migrationBuilder.CreateTable(
                name: "AttributeCategory",
                columns: table => new
                {
                    AttributesId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeCategory", x => new { x.AttributesId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_AttributeCategory_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeCategory_CategoriesId",
                table: "AttributeCategory",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Attributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CategoryId",
                table: "Attributes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Categories_CategoryId",
                table: "Attributes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
