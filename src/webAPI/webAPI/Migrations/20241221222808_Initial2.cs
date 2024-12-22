using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed957578-0ba9-4324-84b6-2945e20c1d99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc8e3c56-7bf4-4b45-a3a6-333f4c65a184");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a2742ac5-4486-4e8b-80bb-618a3472d06b", null, "Admin", "ADMIN" },
                    { "a96a835a-eca1-4bf6-8d57-3716200e5850", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2742ac5-4486-4e8b-80bb-618a3472d06b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a96a835a-eca1-4bf6-8d57-3716200e5850");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ed957578-0ba9-4324-84b6-2945e20c1d99", null, "User", "USER" },
                    { "fc8e3c56-7bf4-4b45-a3a6-333f4c65a184", null, "Admin", "ADMIN" }
                });
        }
    }
}
