using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandlelisteData.Migrations
{
    /// <inheritdoc />
    public partial class manytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Varer_Handlelister_HandlelisteId",
                table: "Varer");

            migrationBuilder.DropIndex(
                name: "IX_Varer_HandlelisteId",
                table: "Varer");

            migrationBuilder.DropColumn(
                name: "HandlelisteId",
                table: "Varer");

            migrationBuilder.CreateTable(
                name: "HandlelisteVare",
                columns: table => new
                {
                    HandlelisterHandlelisteId = table.Column<int>(type: "int", nullable: false),
                    VarerVareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandlelisteVare", x => new { x.HandlelisterHandlelisteId, x.VarerVareId });
                    table.ForeignKey(
                        name: "FK_HandlelisteVare_Handlelister_HandlelisterHandlelisteId",
                        column: x => x.HandlelisterHandlelisteId,
                        principalTable: "Handlelister",
                        principalColumn: "HandlelisteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HandlelisteVare_Varer_VarerVareId",
                        column: x => x.VarerVareId,
                        principalTable: "Varer",
                        principalColumn: "VareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HandlelisteVare_VarerVareId",
                table: "HandlelisteVare",
                column: "VarerVareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HandlelisteVare");

            migrationBuilder.AddColumn<int>(
                name: "HandlelisteId",
                table: "Varer",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 1,
                column: "HandlelisteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 2,
                column: "HandlelisteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 3,
                column: "HandlelisteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 4,
                column: "HandlelisteId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Varer_HandlelisteId",
                table: "Varer",
                column: "HandlelisteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Varer_Handlelister_HandlelisteId",
                table: "Varer",
                column: "HandlelisteId",
                principalTable: "Handlelister",
                principalColumn: "HandlelisteId");
        }
    }
}
