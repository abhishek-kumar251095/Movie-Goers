using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieGoersIIDAL.Migrations
{
    public partial class MoviesAddOverview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Movies");
        }
    }
}
