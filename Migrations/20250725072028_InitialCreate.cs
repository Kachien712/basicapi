using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a2876ee-b59f-4c7e-bb31-56049f2a8e2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60581322-5da1-4f6d-ad6b-a981196cdf75");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19A13286-CDD6-4EF0-949D-5B1DBAA2DC39", null, "Admin", "ADMIN" },
                    { "90A8C397-01EA-452C-A4BE-183AB18A7F7C", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19A13286-CDD6-4EF0-949D-5B1DBAA2DC39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90A8C397-01EA-452C-A4BE-183AB18A7F7C");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a2876ee-b59f-4c7e-bb31-56049f2a8e2d", null, "User", "USER" },
                    { "60581322-5da1-4f6d-ad6b-a981196cdf75", null, "Admin", "ADMIN" }
                });
        }
    }
}
