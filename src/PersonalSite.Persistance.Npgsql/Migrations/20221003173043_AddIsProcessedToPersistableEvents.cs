using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Persistence.Npgsql.Migrations
{
    public partial class AddIsProcessedToPersistableEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "PersistableEvents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "PersistableEvents");
        }
    }
}
