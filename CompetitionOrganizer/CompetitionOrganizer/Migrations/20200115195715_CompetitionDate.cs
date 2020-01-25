using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionOrganizer.Migrations
{
    public partial class CompetitionDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StarDateTime",
                table: "Competitions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarDateTime",
                table: "Competitions");
        }
    }
}
