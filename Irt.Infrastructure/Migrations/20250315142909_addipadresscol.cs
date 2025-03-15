using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Irt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addipadresscol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "irt");

            // Use direct SQL instead
            migrationBuilder.Sql(@"
                -- Drop the old column if it exists
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""last_modified_by_ip_address"";
                ALTER TABLE datasources DROP COLUMN IF EXISTS ""created_by_ip_address"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""last_modified_by_id_address"";
                ALTER TABLE datasets DROP COLUMN IF EXISTS ""created_by_ip_address"";
                
                -- Add the new columns
                ALTER TABLE datasources 
                ADD COLUMN IF NOT EXISTS ""last_modified_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""created_by_ip"" VARCHAR NOT NULL DEFAULT '';

                ALTER TABLE datasets
                ADD COLUMN IF NOT EXISTS ""last_modified_by_ip"" VARCHAR NOT NULL DEFAULT '',
                ADD COLUMN IF NOT EXISTS ""created_by_ip"" VARCHAR NOT NULL DEFAULT '';

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
