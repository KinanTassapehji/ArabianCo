using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArabianCo.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaTranslation_Areas_CoreId",
                table: "AreaTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslation_Categories_CoreId",
                table: "CategoryTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslation_Cities_CoreId",
                table: "CityTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryTranslation_Countries_CoreId",
                table: "CountryTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryTranslation",
                table: "CountryTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityTranslation",
                table: "CityTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTranslation",
                table: "CategoryTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AreaTranslation",
                table: "AreaTranslation");

            migrationBuilder.RenameTable(
                name: "CountryTranslation",
                newName: "CountryTranslations");

            migrationBuilder.RenameTable(
                name: "CityTranslation",
                newName: "CityTranslations");

            migrationBuilder.RenameTable(
                name: "CategoryTranslation",
                newName: "CategoryTranslations");

            migrationBuilder.RenameTable(
                name: "AreaTranslation",
                newName: "AreaTranslations");

            migrationBuilder.RenameIndex(
                name: "IX_CountryTranslation_CoreId",
                table: "CountryTranslations",
                newName: "IX_CountryTranslations_CoreId");

            migrationBuilder.RenameIndex(
                name: "IX_CityTranslation_CoreId",
                table: "CityTranslations",
                newName: "IX_CityTranslations_CoreId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTranslation_CoreId",
                table: "CategoryTranslations",
                newName: "IX_CategoryTranslations_CoreId");

            migrationBuilder.RenameIndex(
                name: "IX_AreaTranslation_CoreId",
                table: "AreaTranslations",
                newName: "IX_AreaTranslations_CoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryTranslations",
                table: "CountryTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityTranslations",
                table: "CityTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTranslations",
                table: "CategoryTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AreaTranslations",
                table: "AreaTranslations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandTranslations_Brands_CoreId",
                        column: x => x.CoreId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandTranslations_CoreId",
                table: "BrandTranslations",
                column: "CoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaTranslations_Areas_CoreId",
                table: "AreaTranslations",
                column: "CoreId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_Categories_CoreId",
                table: "CategoryTranslations",
                column: "CoreId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslations_Cities_CoreId",
                table: "CityTranslations",
                column: "CoreId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryTranslations_Countries_CoreId",
                table: "CountryTranslations",
                column: "CoreId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaTranslations_Areas_CoreId",
                table: "AreaTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_Categories_CoreId",
                table: "CategoryTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslations_Cities_CoreId",
                table: "CityTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryTranslations_Countries_CoreId",
                table: "CountryTranslations");

            migrationBuilder.DropTable(
                name: "BrandTranslations");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryTranslations",
                table: "CountryTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityTranslations",
                table: "CityTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTranslations",
                table: "CategoryTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AreaTranslations",
                table: "AreaTranslations");

            migrationBuilder.RenameTable(
                name: "CountryTranslations",
                newName: "CountryTranslation");

            migrationBuilder.RenameTable(
                name: "CityTranslations",
                newName: "CityTranslation");

            migrationBuilder.RenameTable(
                name: "CategoryTranslations",
                newName: "CategoryTranslation");

            migrationBuilder.RenameTable(
                name: "AreaTranslations",
                newName: "AreaTranslation");

            migrationBuilder.RenameIndex(
                name: "IX_CountryTranslations_CoreId",
                table: "CountryTranslation",
                newName: "IX_CountryTranslation_CoreId");

            migrationBuilder.RenameIndex(
                name: "IX_CityTranslations_CoreId",
                table: "CityTranslation",
                newName: "IX_CityTranslation_CoreId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTranslations_CoreId",
                table: "CategoryTranslation",
                newName: "IX_CategoryTranslation_CoreId");

            migrationBuilder.RenameIndex(
                name: "IX_AreaTranslations_CoreId",
                table: "AreaTranslation",
                newName: "IX_AreaTranslation_CoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryTranslation",
                table: "CountryTranslation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityTranslation",
                table: "CityTranslation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTranslation",
                table: "CategoryTranslation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AreaTranslation",
                table: "AreaTranslation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaTranslation_Areas_CoreId",
                table: "AreaTranslation",
                column: "CoreId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslation_Categories_CoreId",
                table: "CategoryTranslation",
                column: "CoreId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslation_Cities_CoreId",
                table: "CityTranslation",
                column: "CoreId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryTranslation_Countries_CoreId",
                table: "CountryTranslation",
                column: "CoreId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
