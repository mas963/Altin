using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altin.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class categoryNormalizeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizeName",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizeName",
                table: "Categories");
        }
    }
}
