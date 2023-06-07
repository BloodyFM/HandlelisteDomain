using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandlelisteData.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handlelister",
                columns: table => new
                {
                    HandlelisteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HandlelisteName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handlelister", x => x.HandlelisteId);
                });

            migrationBuilder.CreateTable(
                name: "Varer",
                columns: table => new
                {
                    VareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VareName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandlelisteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Varer", x => x.VareId);
                    table.ForeignKey(
                        name: "FK_Varer_Handlelister_HandlelisteId",
                        column: x => x.HandlelisteId,
                        principalTable: "Handlelister",
                        principalColumn: "HandlelisteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Varer_HandlelisteId",
                table: "Varer",
                column: "HandlelisteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Varer");

            migrationBuilder.DropTable(
                name: "Handlelister");
        }
    }
}
