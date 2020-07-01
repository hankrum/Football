using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballInformationSystem.Api.Migrations
{
    public partial class TeamIdFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_AwayTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Competitions_CompetitionId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamCompetition_Teams_TeamId",
                table: "TeamCompetition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MatchFinished",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Games",
                newName: "IX_Games_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_CompetitionId",
                table: "Games",
                newName: "IX_Games_CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Games",
                newName: "IX_Games_AwayTeamId");

            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "Teams",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "Competitions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GameFinished",
                table: "Games",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "GameId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_AwayTeamId",
                table: "Games",
                column: "AwayTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Competitions_CompetitionId",
                table: "Games",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "CompetitionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCompetition_Teams_TeamId",
                table: "TeamCompetition",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Teams_TeamId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_AwayTeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Competitions_CompetitionId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamCompetition_Teams_TeamId",
                table: "TeamCompetition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_TeamId",
                table: "Competitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "GameFinished",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Matches");

            migrationBuilder.RenameIndex(
                name: "IX_Games_HomeTeamId",
                table: "Matches",
                newName: "IX_Matches_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_CompetitionId",
                table: "Matches",
                newName: "IX_Matches_CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_AwayTeamId",
                table: "Matches",
                newName: "IX_Matches_AwayTeamId");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Teams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "MatchFinished",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Competitions_CompetitionId",
                table: "Matches",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "CompetitionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCompetition_Teams_TeamId",
                table: "TeamCompetition",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
