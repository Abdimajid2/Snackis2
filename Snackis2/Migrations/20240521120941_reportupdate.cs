using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis2.Migrations
{
    /// <inheritdoc />
    public partial class reportupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reports_PostId",
                table: "Reports",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Post_PostId",
                table: "Reports",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Post_PostId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_PostId",
                table: "Reports");
        }
    }
}
