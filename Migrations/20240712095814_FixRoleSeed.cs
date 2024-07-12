using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "179ff16a-ec31-4c6d-bf52-e7c3ef0a0741");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64b4e8a3-0992-4f25-8c57-e779d7f78ebe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b13c0d29-e574-45aa-a49a-1255d41b6d66", "e049d5ec-5e23-4a39-bdb9-d2d73fb654c5", "Admin", "ADMIN" },
                    { "fe7e18f9-1c3c-4f33-8022-2531c3fe66ea", "2f5a60e3-18b7-4ee2-8103-d80f9685bc64", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b13c0d29-e574-45aa-a49a-1255d41b6d66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe7e18f9-1c3c-4f33-8022-2531c3fe66ea");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "179ff16a-ec31-4c6d-bf52-e7c3ef0a0741", null, "Admin", "ADMIN" },
                    { "64b4e8a3-0992-4f25-8c57-e779d7f78ebe", null, "User", "USER" }
                });
        }
    }
}
