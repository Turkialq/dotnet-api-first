using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api_first.Migrations
{
    /// <inheritdoc />
    public partial class usercharacterrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "characters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_characters_userid",
                table: "characters",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_users_userid",
                table: "characters",
                column: "userid",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_users_userid",
                table: "characters");

            migrationBuilder.DropIndex(
                name: "IX_characters_userid",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "characters");
        }
    }
}
