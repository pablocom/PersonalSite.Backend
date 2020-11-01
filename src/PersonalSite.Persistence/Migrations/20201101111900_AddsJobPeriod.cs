using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalSite.Persistence.Migrations
{
    public partial class AddsJobPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JobPeriod_End",
                table: "JobExperiences",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JobPeriod_Start",
                table: "JobExperiences",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobPeriod_End",
                table: "JobExperiences");

            migrationBuilder.DropColumn(
                name: "JobPeriod_Start",
                table: "JobExperiences");
        }
    }
}
