using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Irt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesUnderIrtSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Datasources",
                table: "Datasources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Datasets",
                table: "Datasets");

            migrationBuilder.EnsureSchema(
                name: "irt");

            migrationBuilder.RenameTable(
                name: "Datasources",
                newName: "datasources",
                newSchema: "irt");

            migrationBuilder.RenameTable(
                name: "Datasets",
                newName: "datasets",
                newSchema: "irt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_datasources",
                schema: "irt",
                table: "datasources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_datasets",
                schema: "irt",
                table: "datasets",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_datasources",
                schema: "irt",
                table: "datasources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_datasets",
                schema: "irt",
                table: "datasets");

            migrationBuilder.RenameTable(
                name: "datasources",
                schema: "irt",
                newName: "Datasources");

            migrationBuilder.RenameTable(
                name: "datasets",
                schema: "irt",
                newName: "Datasets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Datasources",
                table: "Datasources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Datasets",
                table: "Datasets",
                column: "Id");
        }
    }
}
