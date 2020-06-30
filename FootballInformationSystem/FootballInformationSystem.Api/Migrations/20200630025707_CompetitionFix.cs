using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballInformationSystem.Api.Migrations
{
    public partial class CompetitionFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TeamCompetition",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "TeamCompetition",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TeamCompetition",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TeamCompetition",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TeamCompetition");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "TeamCompetition");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TeamCompetition");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TeamCompetition");
        }
    }
}
