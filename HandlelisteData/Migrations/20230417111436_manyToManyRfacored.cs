using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandlelisteData.Migrations
{
    /// <inheritdoc />
    public partial class manyToManyRfacored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HandlelisteVare");

            migrationBuilder.CreateTable(
                name: "Vareinstance",
                columns: table => new
                {
                    VareInstanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VareId = table.Column<int>(type: "int", nullable: false),
                    HandlelisteId = table.Column<int>(type: "int", nullable: false),
                    Mengde = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vareinstance", x => x.VareInstanceId);
                    table.ForeignKey(
                        name: "FK_Vareinstance_Handlelister_HandlelisteId",
                        column: x => x.HandlelisteId,
                        principalTable: "Handlelister",
                        principalColumn: "HandlelisteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vareinstance_Varer_VareId",
                        column: x => x.VareId,
                        principalTable: "Varer",
                        principalColumn: "VareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vareinstance_HandlelisteId",
                table: "Vareinstance",
                column: "HandlelisteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vareinstance_VareId",
                table: "Vareinstance",
                column: "VareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vareinstance");

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
    }
}
