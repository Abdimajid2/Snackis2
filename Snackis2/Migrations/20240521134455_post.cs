using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis2.Migrations
{
    /// <inheritdoc />
    public partial class post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Report_PostId",
                table: "Report",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Post_PostId",
                table: "Report",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Post_PostId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_PostId",
                table: "Report");
        }
    }
}
