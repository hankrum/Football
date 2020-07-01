using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballInformationSystem.Api.Migrations
{
    public partial class CompetitionsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Teams_TeamId",
                table: "Competitions");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_TeamId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Competitions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "Competitions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_TeamId",
                table: "Competitions",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Teams_TeamId",
                table: "Competitions",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
