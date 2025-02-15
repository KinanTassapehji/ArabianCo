using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArabianCo.Migrations
{
    /// <inheritdoc />
    public partial class AddShowOrderToAboutUsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowOrder",
                table: "AboutUss",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOrder",
                table: "AboutUss");
        }
    }
}
