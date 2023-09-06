using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArabianCo.Migrations
{
    /// <inheritdoc />
    public partial class updateMaintenanceRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "MaintenanceRequests",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MaintenanceRequests");
        }
    }
}
