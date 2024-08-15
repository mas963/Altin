using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altin.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class categoryOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryOrder",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryOrder",
                table: "Categories");
        }
    }
}
