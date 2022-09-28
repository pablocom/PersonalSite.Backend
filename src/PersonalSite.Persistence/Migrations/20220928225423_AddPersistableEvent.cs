using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSite.Persistence.Migrations
{
    public partial class AddPersistableEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersistableEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullyQualifiedTypeName = table.Column<string>(type: "text", nullable: false),
                    SerializedData = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp(5) with time zone", precision: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistableEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersistableEvents_CreatedAt",
                table: "PersistableEvents",
                column: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersistableEvents");
        }
    }
}
