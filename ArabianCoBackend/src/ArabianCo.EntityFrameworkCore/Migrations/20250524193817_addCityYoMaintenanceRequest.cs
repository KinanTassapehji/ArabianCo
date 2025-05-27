using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArabianCo.Migrations
{
    /// <inheritdoc />
    public partial class addCityYoMaintenanceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Areas_AreaId",
                table: "MaintenanceRequests");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "MaintenanceRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "MaintenanceRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherArea",
                table: "MaintenanceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_CityId",
                table: "MaintenanceRequests",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Areas_AreaId",
                table: "MaintenanceRequests",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Cities_CityId",
                table: "MaintenanceRequests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Areas_AreaId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Cities_CityId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_CityId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "OtherArea",
                table: "MaintenanceRequests");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Areas_AreaId",
                table: "MaintenanceRequests",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
