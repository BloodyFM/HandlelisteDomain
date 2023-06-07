using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandlelisteData.Migrations
{
    /// <inheritdoc />
    public partial class VareinstanceBetterPk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vareinstance",
                table: "Vareinstance");

            migrationBuilder.DropIndex(
                name: "IX_Vareinstance_VareId",
                table: "Vareinstance");

            migrationBuilder.DropColumn(
                name: "VareInstanceId",
                table: "Vareinstance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vareinstance",
                table: "Vareinstance",
                columns: new[] { "VareId", "HandlelisteId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vareinstance",
                table: "Vareinstance");

            migrationBuilder.AddColumn<int>(
                name: "VareInstanceId",
                table: "Vareinstance",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vareinstance",
                table: "Vareinstance",
                column: "VareInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Vareinstance_VareId",
                table: "Vareinstance",
                column: "VareId");
        }
    }
}
