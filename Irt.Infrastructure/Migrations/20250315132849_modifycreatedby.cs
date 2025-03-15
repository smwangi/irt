using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Irt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifycreatedby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "irt");

            // Use direct SQL instead
            migrationBuilder.Sql(@"
                -- Drop the old column if it exists
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""CreatedBy"";
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""LastModifiedBy"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""CreatedBy"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""LastModifiedBy"";
                
                -- Add the new columns
                ALTER TABLE datasources 
                ADD COLUMN IF NOT EXISTS ""created_by_id"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""created_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""created_at"" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                ADD COLUMN IF NOT EXISTS ""created_by_name"" VARCHAR(100) NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""modified_by_id"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""modified_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""modified_at"" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                ADD COLUMN IF NOT EXISTS ""modified_by_name"" VARCHAR NOT NULL DEFAULT '';

                ALTER TABLE datasets 
                ADD COLUMN IF NOT EXISTS ""created_by_id"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""created_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""created_at"" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                ADD COLUMN IF NOT EXISTS ""created_by_name"" VARCHAR(100) NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""modified_by_id"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""modified_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""modified_at"" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                ADD COLUMN IF NOT EXISTS ""modified_by_name"" VARCHAR NOT NULL DEFAULT '';

            ");
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
        }
    }
}
