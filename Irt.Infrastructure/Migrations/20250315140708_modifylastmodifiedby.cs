using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Irt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class  modifylastmodifiedby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
migrationBuilder.EnsureSchema(
                name: "irt");

            // Use direct SQL instead
            migrationBuilder.Sql(@"
                -- Drop the old column if it exists
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""modified_by_id"";
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""modified_by_ip"";
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""modified_at"";
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""modified_by_name"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""modified_by_id"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""modified_by_ip"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""modified_at"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""modified_by_name"";
                
                -- Add the new columns
                ALTER TABLE datasources 
                ADD COLUMN IF NOT EXISTS ""last_modified_by_id"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""last_modified_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""last_modified_at"" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                ADD COLUMN IF NOT EXISTS ""last_modified_by_name"" VARCHAR NOT NULL DEFAULT '';

                ALTER TABLE datasets
                ADD COLUMN IF NOT EXISTS ""last_modified_by_id"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""last_modified_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""last_modified_at"" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                ADD COLUMN IF NOT EXISTS ""last_modified_by_name"" VARCHAR NOT NULL DEFAULT '';

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
