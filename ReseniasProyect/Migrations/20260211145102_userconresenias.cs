using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReseniasProyect.Migrations
{
    /// <inheritdoc />
    public partial class userconresenias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "resenias",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_resenias_UserId",
                table: "resenias",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_resenias_AspNetUsers_UserId",
                table: "resenias",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resenias_AspNetUsers_UserId",
                table: "resenias");

            migrationBuilder.DropIndex(
                name: "IX_resenias_UserId",
                table: "resenias");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "resenias");
        }
    }
}
