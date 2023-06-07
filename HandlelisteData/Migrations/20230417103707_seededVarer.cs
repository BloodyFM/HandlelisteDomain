using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HandlelisteData.Migrations
{
    /// <inheritdoc />
    public partial class seededVarer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Varer",
                columns: new[] { "VareId", "HandlelisteId", "VareName" },
                values: new object[,]
                {
                    { 1, null, "Pizza" },
                    { 2, null, "Cola" },
                    { 3, null, "Rømmedressing" },
                    { 4, null, "Rømme" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Varer",
                keyColumn: "VareId",
                keyValue: 4);
        }
    }
}
