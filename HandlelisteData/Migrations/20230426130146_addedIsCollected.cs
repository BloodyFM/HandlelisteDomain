using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandlelisteData.Migrations
{
    /// <inheritdoc />
    public partial class addedIsCollected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCollected",
                table: "Vareinstance",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCollected",
                table: "Vareinstance");
        }
    }
}
