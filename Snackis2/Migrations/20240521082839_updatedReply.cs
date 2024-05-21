using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis2.Migrations
{
    /// <inheritdoc />
    public partial class updatedReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Reply");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Reply",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
