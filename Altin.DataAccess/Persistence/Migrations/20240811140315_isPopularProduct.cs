using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altin.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class isPopularProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPopular",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPopular",
                table: "Products");
        }
    }
}
