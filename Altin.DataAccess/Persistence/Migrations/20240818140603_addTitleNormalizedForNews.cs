using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altin.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addTitleNormalizedForNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedTitle",
                table: "News",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedTitle",
                table: "News");
        }
    }
}
