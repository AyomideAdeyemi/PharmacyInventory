using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharmacyInventory_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40972614-443c-4acd-a491-a01ab5c84903");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8903f54d-fb36-4e17-98af-224f3fac683a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51190b7d-f8c0-4c50-9ea8-945bb791d289", null, "Admin", "ADMIN" },
                    { "58b55347-7845-41a0-a2cf-2345e9af2f1f", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51190b7d-f8c0-4c50-9ea8-945bb791d289");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58b55347-7845-41a0-a2cf-2345e9af2f1f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40972614-443c-4acd-a491-a01ab5c84903", null, "Admin", "ADMIN" },
                    { "8903f54d-fb36-4e17-98af-224f3fac683a", null, "User", "USER" }
                });
        }
    }
}
