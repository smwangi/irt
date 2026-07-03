using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Irt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "irt");

            migrationBuilder.CreateTable(
                name: "datasources",
                schema: "irt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    DatasourceType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: false),
                    created_by_name = table.Column<string>(type: "text", nullable: false),
                    created_by_app = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_app = table.Column<string>(type: "text", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by_ip = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datasources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "indicator_main_categories",
                schema: "irt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: false),
                    created_by_name = table.Column<string>(type: "text", nullable: false),
                    created_by_app = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_app = table.Column<string>(type: "text", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by_ip = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_indicator_main_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reporting_scopes",
                schema: "irt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: false),
                    created_by_name = table.Column<string>(type: "text", nullable: false),
                    created_by_app = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_app = table.Column<string>(type: "text", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by_ip = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reporting_scopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unit_of_measures",
                schema: "irt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: false),
                    created_by_name = table.Column<string>(type: "text", nullable: false),
                    created_by_app = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_app = table.Column<string>(type: "text", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by_ip = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_of_measures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "indicator_categories",
                schema: "irt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IndicatorMainCategoryId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: false),
                    created_by_name = table.Column<string>(type: "text", nullable: false),
                    created_by_app = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_app = table.Column<string>(type: "text", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by_ip = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_indicator_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_indicator_categories_indicator_main_categories_IndicatorMai~",
                        column: x => x.IndicatorMainCategoryId,
                        principalSchema: "irt",
                        principalTable: "indicator_main_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "indicator_definitions",
                schema: "irt",
                columns: table => new
                {
                    indicator_definition_name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ReportingScopeId = table.Column<string>(type: "text", nullable: false),
                    UnitOfMeasureId = table.Column<string>(type: "text", nullable: false),
                    IndicatorCategoryId = table.Column<string>(type: "text", nullable: false),
                    MinThreshold = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxThreshold = table.Column<decimal>(type: "numeric", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: true),
                    FormulaDescription = table.Column<string>(type: "text", nullable: true),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    DPSIR = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: false),
                    created_by_name = table.Column<string>(type: "text", nullable: false),
                    created_by_app = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_app = table.Column<string>(type: "text", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by_ip = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_indicator_definitions", x => x.indicator_definition_name);
                    table.ForeignKey(
                        name: "FK_indicator_definitions_indicator_categories_IndicatorCategor~",
                        column: x => x.IndicatorCategoryId,
                        principalSchema: "irt",
                        principalTable: "indicator_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_indicator_definitions_reporting_scopes_ReportingScopeId",
                        column: x => x.ReportingScopeId,
                        principalSchema: "irt",
                        principalTable: "reporting_scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_indicator_definitions_unit_of_measures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "irt",
                        principalTable: "unit_of_measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "datasets",
                schema: "irt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DatasourceId = table.Column<string>(type: "text", nullable: false),
                    DatasetType = table.Column<int>(type: "integer", nullable: false),
                    IndicatorDefinitionId = table.Column<string>(type: "text", nullable: false),
                    SpreadJson = table.Column<string>(type: "text", nullable: true),
                    Bindings = table.Column<string>(type: "text", nullable: true),
                    Schedule = table.Column<string>(type: "text", nullable: true),
                    CronExpression = table.Column<string>(type: "text", nullable: true),
                    RelatedIndicatorsIds = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: false),
                    created_by_name = table.Column<string>(type: "text", nullable: false),
                    created_by_app = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: false),
                    last_modified_by_app = table.Column<string>(type: "text", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by_ip = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datasets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_datasets_datasources_DatasourceId",
                        column: x => x.DatasourceId,
                        principalSchema: "irt",
                        principalTable: "datasources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_datasets_indicator_definitions_IndicatorDefinitionId",
                        column: x => x.IndicatorDefinitionId,
                        principalSchema: "irt",
                        principalTable: "indicator_definitions",
                        principalColumn: "indicator_definition_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_datasets_DatasourceId",
                schema: "irt",
                table: "datasets",
                column: "DatasourceId");

            migrationBuilder.CreateIndex(
                name: "IX_datasets_IndicatorDefinitionId",
                schema: "irt",
                table: "datasets",
                column: "IndicatorDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_indicator_categories_IndicatorMainCategoryId",
                schema: "irt",
                table: "indicator_categories",
                column: "IndicatorMainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_indicator_definitions_IndicatorCategoryId",
                schema: "irt",
                table: "indicator_definitions",
                column: "IndicatorCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_indicator_definitions_ReportingScopeId",
                schema: "irt",
                table: "indicator_definitions",
                column: "ReportingScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_indicator_definitions_UnitOfMeasureId",
                schema: "irt",
                table: "indicator_definitions",
                column: "UnitOfMeasureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datasets",
                schema: "irt");

            migrationBuilder.DropTable(
                name: "datasources",
                schema: "irt");

            migrationBuilder.DropTable(
                name: "indicator_definitions",
                schema: "irt");

            migrationBuilder.DropTable(
                name: "indicator_categories",
                schema: "irt");

            migrationBuilder.DropTable(
                name: "reporting_scopes",
                schema: "irt");

            migrationBuilder.DropTable(
                name: "unit_of_measures",
                schema: "irt");

            migrationBuilder.DropTable(
                name: "indicator_main_categories",
                schema: "irt");
        }
    }
}
